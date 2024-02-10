using Common;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using Services.Dtos.Central;
using Services.Dtos.History;
using Microsoft.EntityFrameworkCore;
using Entities.History;
using Common.Exceptions;
using Services.WebFramework.Pagination;
using Common.Utilities;
using Microsoft.IdentityModel.Tokens;
using Services.Services.CentralMessage;

namespace Services.Services.Signal
{
    internal class HistoryService : IHistoryService, IScopedDependency
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICentralMessageService _centralMessageService;

        public HistoryService(ApplicationDbContext dbContext, IMapper mapper, ICentralMessageService centralMessageService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _centralMessageService = centralMessageService;
        }

        //need refactor
        public async Task InsertReceivedData(int centralId, CentralMessageDto centralMessageDto, CancellationToken cancellationToken)
        {
            var packetDateTime = DateTime.Now.Ticks;
            //ecg signal
            var ecgSignalEntities = new List<EcgSignal>();
            foreach (var bed in centralMessageDto.Beds)
            {
                try
                {
                    var createEcgSignalDto = new CreateEcgSignalDto
                    {
                        CentralId = centralId,
                        BedId = bed.BedNumber,
                        DateTime = packetDateTime,
                        EcgLead = bed.EcgLead,
                        EcgFilter = bed.EcgFilter,
                        SignalData = bed.EcgSignal
                    };
                    var entity = _mapper.Map<EcgSignal>(createEcgSignalDto);
                    ecgSignalEntities.Add(entity);
                }
                catch (Exception) { /* ignored */ }
            }
            await InsertEcgSignal(ecgSignalEntities, cancellationToken);
            //parameters
            var ecgParameterEntities = new List<Parameter>();
            foreach (var bed in centralMessageDto.Beds)
            {
                try
                {
                    var createParameterDto = new CreateParameterDto
                    {
                        CentralId = centralId,
                        BedId = bed.BedNumber,
                        DateTime = packetDateTime,
                        Hr = bed.Hr,
                        Rr = bed.Rr,
                        Spo2 = bed.Spo2,
                        T1 = bed.T1,
                        T2 = bed.T2,
                        Dt = bed.Dt,
                        Ibp1Sys = bed.Ibp1Sys,
                        Ibp1Dia = bed.Ibp1Dia,
                        Ibp1Map = bed.Ibp1Map,
                        Ibp2Sys = bed.Ibp2Sys,
                        Ibp2Dia = bed.Ibp2Dia,
                        Ibp2Map = bed.Ibp2Map,
                        Awrr = bed.Awrr,
                        EtcO2 = bed.EtcO2,
                        FiCo2 = bed.Fico2,
                        NibpSys = bed.NibpSys,
                        NibpDia = bed.NibpDia,
                        NibpMap = bed.NibpMap
                    };
                    var entity = _mapper.Map<Parameter>(createParameterDto);
                    ecgParameterEntities.Add(entity);
                }
                catch (Exception) { /* ignored */ }
            }
            await InsertParameters(ecgParameterEntities, cancellationToken);
            var ecgAlarmEntities = new List<Alarm>();
            foreach (var bed in centralMessageDto.Beds)
            {
                foreach (var alarm in bed.AlarmList.Where(alarm => !alarm.Code.Trim().IsNullOrEmpty()))
                {
                    try
                    {
                        var createAlarmDto = new CreateAlarmDto
                        {
                            CentralId = centralId,
                            BedId = bed.BedNumber,
                            DateTime = packetDateTime,
                            AlarmType = bed.AlarmType.Trim(),
                            Code = alarm.Code,
                            Level = alarm.Level
                        };
                        var entity = _mapper.Map<Alarm>(createAlarmDto);
                        ecgAlarmEntities.Add(entity);
                    }
                    catch (Exception) { /* ignored */ }
                }
            }
            await InsertAlarms(ecgAlarmEntities, cancellationToken);
        }

        private async Task InsertEcgSignal(IEnumerable<EcgSignal> createEcgSignalDto, CancellationToken cancellationToken)
        {
            await _dbContext.Set<EcgSignal>().AddRangeAsync(createEcgSignalDto, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task InsertParameters(IEnumerable<Parameter> createParameterDto, CancellationToken cancellationToken)
        {
            await _dbContext.Set<Parameter>().AddRangeAsync(createParameterDto, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task InsertAlarms(IEnumerable<Alarm> createAlarmEntities, CancellationToken cancellationToken)
        {
            await _dbContext.Set<Alarm>().AddRangeAsync(createAlarmEntities, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        //public Task<List<EcgSignalDto>> GetEcgSignal(GetEcgSignalDto getEcgSignalDto, CancellationToken cancellationToken)
        //{
        //    var query = this._dbContext
        //        .Set<EcgSignal>()
        //        .AsNoTracking()
        //        .AsSingleQuery()
        //        .Where(s => s.CentralId == getEcgSignalDto.CentralId && s.BedId == getEcgSignalDto.BedId && s.Lead == getEcgSignalDto.Lead);
        //    if (query == null || !query.Any())
        //        throw new NotFoundException("There is no data.");
        //    return _mapper.ProjectTo<EcgSignalDto>(query).ToListAsync(cancellationToken);
        //}

        public async Task<HistoryResponseDto> GetHistory(HistoryRequestDto historyRequestDto, CancellationToken cancellationToken)
        {
            const int samplesCountInOneSecond = 200;
            var durationTimeBasedOnSamplesCount = (int)Math.Ceiling((double)historyRequestDto.SamplesCount / samplesCountInOneSecond);
            var signalData = await GetHistorySignal(historyRequestDto, durationTimeBasedOnSamplesCount, cancellationToken);
            var signalDataSamples = signalData.Aggregate("", (current, entity) => current + entity.SignalData);
            if (signalDataSamples.Length >= historyRequestDto.SamplesCount * 2)
                signalDataSamples = signalDataSamples[..(historyRequestDto.SamplesCount * 2)];
            //.Substring(0, historyRequestDto.SamplesCount * 2) means => [..(historyRequestDto.SamplesCount * 2)]
            var parameterData = await GetHistoryParameter(historyRequestDto, durationTimeBasedOnSamplesCount, cancellationToken);
            return new HistoryResponseDto
            {
                DateTime = signalData.First().DateTime,
                SignalData = signalDataSamples,
                Parameters = parameterData
            };
        }

        private async Task<List<EcgSignal>> GetHistorySignal(HistoryRequestDto historyRequestDto, int durationTimeBasedOnSamplesCount, CancellationToken cancellationToken)
        {
            var signalQuery = _dbContext
                .Set<EcgSignal>()
                .AsNoTracking()
                .AsSingleQuery()
                .Where(s => s.CentralId == historyRequestDto.CentralId &&
                            s.BedId == historyRequestDto.BedId.ToString() && s.EcgLead == historyRequestDto.Lead.ToString()
                            && s.DateTime >= historyRequestDto.StartDateTime).Take(durationTimeBasedOnSamplesCount);
            if (signalQuery == null || !signalQuery.Any())
                throw new NotFoundException("There is no signal data.");
            return await signalQuery.ToListAsync(cancellationToken);
        }

        private async Task<List<HistoryResponseParameterDto>> GetHistoryParameter(HistoryRequestDto historyRequestDto, int durationTimeBasedOnSamplesCount, CancellationToken cancellationToken)
        {
            var parameterQuery = _dbContext
                .Set<Parameter>()
                .AsNoTracking()
                .AsSingleQuery()
                .Where(s => s.CentralId == historyRequestDto.CentralId && s.BedId == historyRequestDto.BedId.ToString()
                    && s.DateTime >= historyRequestDto.StartDateTime).Take(durationTimeBasedOnSamplesCount)
                .ProjectTo<HistoryResponseParameterDto>(_mapper.ConfigurationProvider);
            if (parameterQuery == null || !parameterQuery.Any())
                throw new NotFoundException("There is no parameter data.");
            return await parameterQuery.ToListAsync(cancellationToken);
        }

        public async Task<PagedQueryable<HistoryAlarmResponseDto>> GetAlarmHistory(string? queries, CancellationToken cancellationToken)
        {
            var query = _dbContext.Set<Alarm>().AsNoTracking();
            var alarms = _mapper.ProjectTo<HistoryAlarmResponseDto>(query);
            if (alarms == null)
                throw new BadRequestException("داده ای یافت نشد.");
            var paginationParams = OdataUtils.GetSkipLimit(queries, query);
            var result = new PagedQueryable<HistoryAlarmResponseDto>
            {
                Data = alarms,
                CurrentPage = paginationParams.CurrentPageNumber,
                PageSize = paginationParams.Limit,
                TotalCount = paginationParams.TotalCount,
                TotalPages = (int)Math.Ceiling(paginationParams.TotalCount / (double)paginationParams.Limit)
            };
            return result;
        }
    }
}
