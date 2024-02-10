using Common;
using Common.Utilities;
using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Dtos.Central;

namespace Services.Services.CentralMessage
{
    internal class CentralMessageService : ICentralMessageService, IScopedDependency
    {
        #region Private Fields
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<Entities.User.User> _userManager;
        private static readonly List<KeyValuePair<int, int>> RegisteredUsersAndCentrals = new();
        private static readonly List<CentralBedsDto> CentralActiveBeds = new();
        #endregion

        #region ctor
        public CentralMessageService(ApplicationDbContext dbContext, UserManager<Entities.User.User> userManager)
        {
            this._dbContext = dbContext;
            this._userManager = userManager;
        }

        #endregion

        #region Public Methods
        public async Task<List<string>> FindCentralMessageRelatedToSpecificUsers(int centralId, int bedsCount, CancellationToken cancellationToken)
        {
            this.FillCentralActiveBeds(centralId, bedsCount);
            if (RegisteredUsersAndCentrals.Count == 0) return new List<string>();
            var admins = await this._userManager.GetUsersInRoleAsync(RolesEnum.Admin.ToString());
            var superAdmins = await this._userManager.GetUsersInRoleAsync(RolesEnum.SuperAdmin.ToString());

            var userIds = await _dbContext.Set<Entities.UserCentral.UserCentral>().AsNoTracking()
                .Where(s => s.CentralId == centralId)
                .Select(s => s.UserId.ToString()).ToListAsync(cancellationToken);

            userIds.AddRange(admins.Select(s => s.Id.ToString()).ToList());
            userIds.AddRange(superAdmins.Select(s => s.Id.ToString()).ToList());

            var onlineCentralUsers = RegisteredUsersAndCentrals
                .Where(s => s.Value == centralId).Select(s => s.Key).ToList();

            var result = onlineCentralUsers.Where(s => userIds.Contains(s.ToString())).Select(s => s.ToString()).ToList();
            return result;
        }

        public void GetDesiredCentral(int userId, int centralId)
        {
            if (RegisteredUsersAndCentrals.Any(s => s.Key == userId))
            {
                RegisteredUsersAndCentrals.RemoveAll(s => s.Key == userId);
                RegisteredUsersAndCentrals.Add(new KeyValuePair<int, int>(userId, centralId));
            }
            else if (!RegisteredUsersAndCentrals.Any(s => s.Key == userId && s.Value == centralId))
                RegisteredUsersAndCentrals.Add(new KeyValuePair<int, int>(userId, centralId));
        }


        public void ReleaseDesiredCentral(int userId, int centralId) =>
            RegisteredUsersAndCentrals.Remove(
                RegisteredUsersAndCentrals.FirstOrDefault(s => s.Key == userId && s.Value == centralId));

        public int GetCentralActiveBeds(int centralId)
        {
            if (this.CentralBedsAreNotActive(centralId))
                return 0;
            var target = CentralActiveBeds.Where(p => p.CentralId == centralId).ToList();
            return target.Any() ? target[0].ActiveBedCount : 0;
        }

        public async Task<CentralMessageDto> GetAlarmsWithMessage(CentralMessageDto requestData)
        {
            //Trace.WriteLine("------------------------------------------------");
            //Trace.WriteLine($"####### {requestData.Beds[0].AlarmList[0].Message}-{requestData.Beds[0].AwrrBlink}-{requestData.Beds[0].Fico2Blink}-{requestData.Beds[0].AwrrBlink}");
            var resultBeds = new List<BedDetailDto>();
            foreach (var bed in requestData.Beds)
            {
                var bedWithNewInformation = this.MakeParametersBlink(bed);
                bedWithNewInformation.AlarmList = this.FillMessageOfAlarms(bedWithNewInformation.AlarmType, bedWithNewInformation.AlarmList);
                resultBeds.Add(bedWithNewInformation);
            }
            requestData.Beds = resultBeds;
            //Trace.WriteLine($"******* {requestData.Beds[0].AlarmList[0].Message}-{requestData.Beds[0].AwrrBlink}-{requestData.Beds[0].Fico2Blink}-{requestData.Beds[0].AwrrBlink}");
            //Trace.WriteLine("+++++++++++++++++++++++++++++++++++++++++");
            return requestData;
        }

        /*bottom method(MakeParametersBlink) is from Ms.Asgari(Central unit)*/
        private BedDetailDto MakeParametersBlink(BedDetailDto bed)
        {
            foreach (var alarmDto in bed.AlarmList)
            {
                var intCode = int.Parse(alarmDto.Code);
                if (intCode >= (int)AlarmMessages1.IBP1_PULSE_TOO_HIGH && intCode <= (int)AlarmMessages1.IBP4_PULSE_HIGH)
                {
                    if (bed.HrSource == "1")
                    {
                        if (intCode == (int)AlarmMessages1.IBP1_PULSE_TOO_LOW || intCode == (int)AlarmMessages1.IBP1_PULSE_LOW || intCode == (int)AlarmMessages1.IBP1_PULSE_TOO_HIGH || intCode == (int)AlarmMessages1.IBP1_PULSE_HIGH)
                        {
                            if (bed.PrSent == "1")
                            {
                                bed.HRBlink = true;
                            }
                        }
                    }
                    else if (bed.HrSource == "2")
                    {
                        if (intCode == (int)AlarmMessages1.IBP2_PULSE_TOO_LOW || intCode == (int)AlarmMessages1.IBP2_PULSE_LOW || intCode == (int)AlarmMessages1.IBP2_PULSE_TOO_HIGH || intCode == (int)AlarmMessages1.IBP2_PULSE_HIGH)
                        {
                            if (bed.PrSent == "1")
                            {
                                bed.HRBlink = true;
                            }
                        }
                    }
                    else if (bed.HrSource == "3")
                    {
                        if (intCode == (int)AlarmMessages1.IBP3_PULSE_TOO_LOW || intCode == (int)AlarmMessages1.IBP3_PULSE_LOW || intCode == (int)AlarmMessages1.IBP3_PULSE_TOO_HIGH || intCode == (int)AlarmMessages1.IBP3_PULSE_HIGH)
                        {
                            if (bed.PrSent == "1")
                            {
                                bed.HRBlink = true;
                            }
                        }
                    }
                    else if (bed.HrSource == "4")
                    {
                        if (intCode == (int)AlarmMessages1.IBP4_PULSE_TOO_LOW || intCode == (int)AlarmMessages1.IBP4_PULSE_LOW || intCode == (int)AlarmMessages1.IBP4_PULSE_TOO_HIGH || intCode == (int)AlarmMessages1.IBP4_PULSE_HIGH)
                        {
                            if (bed.PrSent == "1")
                            {
                                bed.HRBlink = true;
                            }
                        }
                    }
                }
                switch ((AlarmMessages1)intCode)
                {
                    case AlarmMessages1.PR_TOO_LOW:
                    case AlarmMessages1.PR_TOO_HIGH:
                    case AlarmMessages1.PR_LOW:
                    case AlarmMessages1.PR_HIGH:
                        if (bed.PrSent == "1")
                            bed.HRBlink = true;
                        break;
                    case AlarmMessages1.HR_TOO_LOW:
                    case AlarmMessages1.HR_TOO_HIGH:
                    case AlarmMessages1.HR_LOW:
                    case AlarmMessages1.HR_HIGH:
                        bed.HRBlink = true;
                        bed.AsysFlag = "1";
                        break;
                    case AlarmMessages1.PERCENTAGESIGNSPO2_TOO_LOW:
                    case AlarmMessages1.PERCENTAGESIGNSPO2_TOO_HIGH:
                    case AlarmMessages1.PERCENTAGESIGNSPO2_LOW:
                    case AlarmMessages1.PERCENTAGESIGNSPO2_HIGH:
                        bed.Spo2Blink = true;
                        break;
                    case AlarmMessages1.IBP1_SYS_TOO_LOW:
                    case AlarmMessages1.IBP1_SYS_LOW:
                    case AlarmMessages1.IBP1_SYS_TOO_HIGH:
                    case AlarmMessages1.IBP1_SYS_HIGH:
                        bed.Ibp1SysBlink = true;
                        break;
                    case AlarmMessages1.IBP1_DIA_TOO_LOW:
                    case AlarmMessages1.IBP1_DIA_LOW:
                    case AlarmMessages1.IBP1_DIA_TOO_HIGH:
                    case AlarmMessages1.IBP1_DIA_HIGH:
                        bed.Ibp1DiaBlink = true;
                        break;
                    case AlarmMessages1.IBP1_MEAN_TOO_LOW:
                    case AlarmMessages1.IBP1_MEAN_LOW:
                    case AlarmMessages1.IBP1_MEAN_TOO_HIGH:
                    case AlarmMessages1.IBP1_MEAN_HIGH:
                        bed.Ibp1MapBlink = true;
                        break;
                    case AlarmMessages1.IBP2_SYS_TOO_LOW:
                    case AlarmMessages1.IBP2_SYS_LOW:
                    case AlarmMessages1.IBP2_SYS_TOO_HIGH:
                    case AlarmMessages1.IBP2_SYS_HIGH:
                        bed.Ibp2SysBlink = true;
                        break;
                    case AlarmMessages1.IBP2_DIA_TOO_LOW:
                    case AlarmMessages1.IBP2_DIA_LOW:
                    case AlarmMessages1.IBP2_DIA_TOO_HIGH:
                    case AlarmMessages1.IBP2_DIA_HIGH:
                        bed.Ibp2DiaBlink = true;
                        break;
                    case AlarmMessages1.IBP2_MEAN_TOO_LOW:
                    case AlarmMessages1.IBP2_MEAN_LOW:
                    case AlarmMessages1.IBP2_MEAN_TOO_HIGH:
                    case AlarmMessages1.IBP2_MEAN_HIGH:
                        bed.Ibp2MapBlink = true;
                        break;
                    case AlarmMessages1.T1_TOO_LOW:
                    case AlarmMessages1.T1_TOO_HIGH:
                    case AlarmMessages1.T1_LOW:
                    case AlarmMessages1.T1_HIGH:
                        bed.T1Blink = true;
                        break;
                    case AlarmMessages1.T2_TOO_LOW:
                    case AlarmMessages1.T2_TOO_HIGH:
                    case AlarmMessages1.T2_LOW:
                    case AlarmMessages1.T2_HIGH:
                        bed.T2Blink = true;
                        break;
                    case AlarmMessages1.DT_TOO_LOW:
                    case AlarmMessages1.DT_TOO_HIGH:
                    case AlarmMessages1.DT_LOW:
                    case AlarmMessages1.DT_HIGH:
                        bed.DtBlink = true;
                        break;
                    case AlarmMessages1.RR_TOO_LOW:
                    case AlarmMessages1.RR_LOW:
                    case AlarmMessages1.RR_HIGH:
                        bed.RrBlink = true;
                        break;
                    case AlarmMessages1.NIBP_SYS_TOO_LOW:
                    case AlarmMessages1.NIBP_SYS_TOO_HIGH:
                    case AlarmMessages1.NIBP_SYS_LOW:
                    case AlarmMessages1.NIBP_SYS_HIGH:
                        bed.NibpSysBlink = true;
                        break;
                    case AlarmMessages1.NIBP_DIA_TOO_LOW:
                    case AlarmMessages1.NIBP_DIA_TOO_HIGH:
                    case AlarmMessages1.NIBP_DIA_LOW:
                    case AlarmMessages1.NIBP_DIA_HIGH:
                        bed.NibpDiaBlink = true;
                        break;
                    case AlarmMessages1.NIBP_MAP_TOO_LOW:
                    case AlarmMessages1.NIBP_MAP_TOO_HIGH:
                    case AlarmMessages1.NIBP_MAP_LOW:
                    case AlarmMessages1.NIBP_MAP_HIGH:
                        bed.NibpMapBlink = true;
                        break;
                    case AlarmMessages1.FICO2_TOO_LOW:
                    case AlarmMessages1.FICO2_LOW:
                    case AlarmMessages1.FICO2_TOO_HIGH:
                    case AlarmMessages1.FICO2_HIGH:
                        bed.Fico2Blink = true;
                        break;
                    case AlarmMessages1.ETCO2_TOO_LOW:
                    case AlarmMessages1.ETCO2_LOW:
                    case AlarmMessages1.ETCO2_TOO_HIGH:
                    case AlarmMessages1.ETCO2_HIGH:
                        bed.EtcO2Blink = true;
                        break;
                    case AlarmMessages1.AWRR_TOO_LOW:
                    case AlarmMessages1.AWRR_TOO_HIGH:
                    case AlarmMessages1.AWRR_LOW:
                    case AlarmMessages1.AWRR_HIGH:
                        bed.AwrrBlink = true;
                        break;
                }
            }
            return bed;
        }

        private List<AlarmDto> FillMessageOfAlarms(string bedAlarmType, List<AlarmDto> bedAlarmList)
        {
            foreach (var alarm in bedAlarmList)
                alarm.Message = alarm.Code.ToAlarmMessage(bedAlarmType);
            return bedAlarmList;
        }
        #endregion

        #region Private Methods
        private void FillCentralActiveBeds(int centralId, int bedsCount)
        {
            if (CentralActiveBeds.Any(s => s.CentralId == centralId))
            {
                CentralActiveBeds.RemoveAll(s => s.CentralId == centralId);
                CentralActiveBeds.Add(new CentralBedsDto
                {
                    ActiveBedCount = bedsCount,
                    CentralId = centralId,
                    LastAdditionTime = DateTime.Now.Ticks
                });
            }
            // check for invalid data
            else
                CentralActiveBeds.Add(new CentralBedsDto
                {
                    ActiveBedCount = bedsCount,
                    CentralId = centralId,
                    LastAdditionTime = DateTime.Now.Ticks
                });
        }

        private bool CentralBedsAreNotActive(int centralId)
        {
            var selectedCentral = CentralActiveBeds.FirstOrDefault(s => s.CentralId == centralId);
            if (selectedCentral == null) return true;
            if (selectedCentral.LastAdditionTime >= DateTime.Now.AddSeconds(-5).Ticks) return false;
            selectedCentral.ActiveBedCount = 0;
            return true;
        }
        #endregion
    }
}
