namespace Common.Test
{
    public class AlarmsEnumTests
    {
        [Fact]
        public void AlarmMessages0_Enum_Values_Should_Be_Correct()
        {
            Assert.Equal(0, (int)AlarmMessages0.ECG_NOCABLE_);
            Assert.Equal(1, (int)AlarmMessages0.ECG_CHECK_C);
            Assert.Equal(2, (int)AlarmMessages0.ECG_CHECK_RA);
            Assert.Equal(3, (int)AlarmMessages0.ECG_CHECK_LL);
            Assert.Equal(4, (int)AlarmMessages0.ECG_CHECK_LA);
            Assert.Equal(5, (int)AlarmMessages0.ECG_SIGNAL_WEAK);
            Assert.Equal(6, (int)AlarmMessages0.ECG_CHECK_RL_OR_ALL);
            Assert.Equal(7, (int)AlarmMessages0.ECG_CHECK_LL_OR_ALL);
            Assert.Equal(8, (int)AlarmMessages0.ECG_CHECK_LA_OR_ALL);
            Assert.Equal(9, (int)AlarmMessages0.ECG_CHECK_RA_OR_ALL);
            Assert.Equal(10, (int)AlarmMessages0.ECG_CHECK_RL);
            Assert.Equal(11, (int)AlarmMessages0.RELEARN);
            Assert.Equal(12, (int)AlarmMessages0.CAN_NOT_LEARN);
            Assert.Equal(13, (int)AlarmMessages0.ECG_NOISE);

            Assert.Equal(30, (int)AlarmMessages0.ECG_ASYSTOLE);
            Assert.Equal(31, (int)AlarmMessages0.VFIB_ARRHYTHMIA);
            Assert.Equal(32, (int)AlarmMessages0.VTAC_ARRHYTHMIA);
            Assert.Equal(33, (int)AlarmMessages0.RUN_ARRHYTHMIA);
            Assert.Equal(34, (int)AlarmMessages0.AIVR_ARRHYTHMIA);
            Assert.Equal(35, (int)AlarmMessages0.COUPLET_ARRHYTHMIA);
            Assert.Equal(36, (int)AlarmMessages0.BIGEMINY_ARRHYTHMIA);
            Assert.Equal(37, (int)AlarmMessages0.TRIGEMINY_ARRHYTHMIA);
            Assert.Equal(38, (int)AlarmMessages0.TACHY_ARRHYTHMIA);
            Assert.Equal(39, (int)AlarmMessages0.BRADY_ARRHYTHMIA);
            Assert.Equal(40, (int)AlarmMessages0.PAUS_ARRHYTHMIA);
            Assert.Equal(41, (int)AlarmMessages0.FREQUENT_PVCs);
            Assert.Equal(42, (int)AlarmMessages0.AFIB_ARRHYTHMIA);
            Assert.Equal(43, (int)AlarmMessages0.HROUTOFLIMIT);

            Assert.Equal(60, (int)AlarmMessages0.CO2_SYSTEM_FAULT_1);
            Assert.Equal(61, (int)AlarmMessages0.CO2_SYSTEM_FAULT_2);
            Assert.Equal(62, (int)AlarmMessages0.CO2_SYSTEM_FAULT_3);
            Assert.Equal(63, (int)AlarmMessages0.CO2_SYSTEM_FAULT_4);
            Assert.Equal(64, (int)AlarmMessages0.CO2_SYSTEM_FAULT_51);
            Assert.Equal(65, (int)AlarmMessages0.CO2_SYSTEM_FAULT_57);
            Assert.Equal(66, (int)AlarmMessages0.CO2_SYSTEM_FAULT_60);
            Assert.Equal(67, (int)AlarmMessages0.CO2_SYSTEM_FAULT_65);
            Assert.Equal(68, (int)AlarmMessages0.CO2_SYSTEM_FAULT_70);
            Assert.Equal(69, (int)AlarmMessages0.CO2_SYSTEM_FAULT_71);
            Assert.Equal(70, (int)AlarmMessages0.CO2_SYSTEM_FAULT_40);
            Assert.Equal(71, (int)AlarmMessages0.CO2_SYSTEM_FAULT_44);
            Assert.Equal(72, (int)AlarmMessages0.CO2_SYSTEM_FAULT_46);
            Assert.Equal(73, (int)AlarmMessages0.CO2_SYSTEM_FAULT_47);
            Assert.Equal(74, (int)AlarmMessages0.CO2_SYSTEM_FAULT_4B);
            Assert.Equal(75, (int)AlarmMessages0.CO2_SYSTEM_FAULT_4C);
            Assert.Equal(76, (int)AlarmMessages0.CO2_SYSTEM_FAULT_4D);
            Assert.Equal(77, (int)AlarmMessages0.CO2_SYSTEM_FAULT_4E);
            Assert.Equal(78, (int)AlarmMessages0.CO2_SYSTEM_FAULT_4F);
            Assert.Equal(79, (int)AlarmMessages0.CO2_SYSTEM_FAULT_80);
            Assert.Equal(80, (int)AlarmMessages0.CO2_SYSTEM_FAULT_84);
            Assert.Equal(81, (int)AlarmMessages0.CO2_SYSTEM_FAULT_85);
            Assert.Equal(82, (int)AlarmMessages0.CO2_SENSOR_STANDBY_MODE);
            Assert.Equal(83, (int)AlarmMessages0.CO2_NO_WATER_TRAP);
            Assert.Equal(84, (int)AlarmMessages0.CO2_NO_MODULE);
            Assert.Equal(85, (int)AlarmMessages0.CO2_NO_SENSOR);
            Assert.Equal(86, (int)AlarmMessages0.CO2_CHECK_SAMPLE_LINE);
            Assert.Equal(87, (int)AlarmMessages0.CO2_NO_ADAPTOR);
            Assert.Equal(88, (int)AlarmMessages0.CO2_INVALID);
            Assert.Equal(89, (int)AlarmMessages0.CO2_INVALID_AMBIENT_PRESSURE);
            Assert.Equal(90, (int)AlarmMessages0.CO2_INVALID_AMBIENT_TEMPERATURE);
            Assert.Equal(91, (int)AlarmMessages0.CO2_ROOM_AIR_CALIB_REQUIRED);
            Assert.Equal(92, (int)AlarmMessages0.CO2_ACCURANCY_INVALID_PLEASE_ZERO);
            Assert.Equal(93, (int)AlarmMessages0.CO2_CO2_INLET_OCCLUDE);
            Assert.Equal(94, (int)AlarmMessages0.CO2_EXHUST_OCCLUDE);
            Assert.Equal(95, (int)AlarmMessages0.CO2_UNEXPECTED_REVERSEF_LOW);
            Assert.Equal(96, (int)AlarmMessages0.CO2_CO2_CULATION_ERROR);
            Assert.Equal(97, (int)AlarmMessages0.CO2_SYSTEM_FAULT_66);
            Assert.Equal(98, (int)AlarmMessages0.CO2_SYSTEM_FAULT_051);
            Assert.Equal(99, (int)AlarmMessages0.CO2_UN_EXPECTED_FORWARD_FLOW);
            Assert.Equal(100, (int)AlarmMessages0.CO2_CHECK_SAMPLING_LINE);
            Assert.Equal(101, (int)AlarmMessages0.CO2_SAMPLING_LINE_CLOGGED);
            Assert.Equal(102, (int)AlarmMessages0.CO2_REPLACE_ADAPTOR);
            Assert.Equal(103, (int)AlarmMessages0.CO2_NO_ADAPTER);

            Assert.Equal(110, (int)AlarmMessages0.ETCO2_OUT_OF_LIMIT);
            Assert.Equal(111, (int)AlarmMessages0.FICO2_OUT_OF_LIMIT);
            Assert.Equal(112, (int)AlarmMessages0.AWRR_OUT_OF_LIMIT);
            Assert.Equal(113, (int)AlarmMessages0.CO2_RESP_APNEA);

            Assert.Equal(120, (int)AlarmMessages0.RESP_CHECK_LEADS);
            Assert.Equal(121, (int)AlarmMessages0.RESP_SIGNAL_WEAK);

            Assert.Equal(130, (int)AlarmMessages0.RESP_APNEA);

            Assert.Equal(135, (int)AlarmMessages0.RR_OUT_OF_LIMIT);

            Assert.Equal(140, (int)AlarmMessages0.NIBP_MODE_ERROR);
            Assert.Equal(141, (int)AlarmMessages0.NIBP_SELF_TEST_FAILED);
            Assert.Equal(142, (int)AlarmMessages0.NIBP_LOOSE_CUFF);
            Assert.Equal(143, (int)AlarmMessages0.NIBP_AIR_LEAK);
            Assert.Equal(144, (int)AlarmMessages0.NIBP_AIR_PRESSURE_ERROR);
            Assert.Equal(145, (int)AlarmMessages0.NIBP_SIGNAL_WEAK);
            Assert.Equal(146, (int)AlarmMessages0.NIBP_RANGE_EXCEED);
            Assert.Equal(147, (int)AlarmMessages0.NIBP_EXESSIVE_MOTION);
            Assert.Equal(148, (int)AlarmMessages0.NIBP_OVER_PRESSURE_SENSED);
            Assert.Equal(149, (int)AlarmMessages0.NIBP_SIGNAL_SATURATED);
            Assert.Equal(150, (int)AlarmMessages0.NIBP_PNEUMATIC_LEAK);
            Assert.Equal(151, (int)AlarmMessages0.NIBP_SYSTEM_FAILURE);
            Assert.Equal(152, (int)AlarmMessages0.NIBP_TIMEOUT);
            Assert.Equal(153, (int)AlarmMessages0.NIBP_PRESSURE_ERROR);
            Assert.Equal(154, (int)AlarmMessages0.NIBP_DEFECT);
            Assert.Equal(155, (int)AlarmMessages0.NIBP_STOP_PRESSED);
            Assert.Equal(156, (int)AlarmMessages0.NIBP_STOP);
            Assert.Equal(157, (int)AlarmMessages0.NIBP_LEAKAGE_OK);
            Assert.Equal(158, (int)AlarmMessages0.NIBP_NO_MODULE);
            Assert.Equal(159, (int)AlarmMessages0.NIBP_LOW_BATTERY);
            Assert.Equal(160, (int)AlarmMessages0.NIBP_MODULE_DEFECT);
            Assert.Equal(161, (int)AlarmMessages0.NIBP_MODULE_ERROR);
            Assert.Equal(162, (int)AlarmMessages0.NIBP_PACKET_ERROR);
            Assert.Equal(163, (int)AlarmMessages0.NIBP_STOP_ERROR);

            Assert.Equal(170, (int)AlarmMessages0.NIBP_MAP_OUT_OF_LIMIT);
            Assert.Equal(171, (int)AlarmMessages0.NIBP_DIA_OUT_OF_LIMIT);
            Assert.Equal(172, (int)AlarmMessages0.NIBP_SYS_OUT_OF_LIMIT);

            Assert.Equal(190, (int)AlarmMessages0.IBP2_NO_SENSOR);
            Assert.Equal(191, (int)AlarmMessages0.IBP2_SEARCH);

            Assert.Equal(195, (int)AlarmMessages0.IBP2_ADJUST_SCALE);
            Assert.Equal(196, (int)AlarmMessages0.IBP2_STATIC_PRESSURE);
            Assert.Equal(197, (int)AlarmMessages0.IBP2_DEFECT);

            Assert.Equal(210, (int)AlarmMessages0.IBP2_SYS_OUT_OF_LIMIT);
            Assert.Equal(211, (int)AlarmMessages0.IBP2_DIA_OUT_OF_LIMIT);
            Assert.Equal(212, (int)AlarmMessages0.IBP2_MAP_OUT_OF_LIMIT);

            Assert.Equal(220, (int)AlarmMessages0.IBP1_NO_SENSOR);
            Assert.Equal(221, (int)AlarmMessages0.IBP1_SEARCH);
            Assert.Equal(222, (int)AlarmMessages0.IBP1_ADJUST_SCALE);
            Assert.Equal(223, (int)AlarmMessages0.IBP1_STATIC_PRESSURE);
            Assert.Equal(224, (int)AlarmMessages0.IBP1_DEFECT);

            Assert.Equal(230, (int)AlarmMessages0.IBP1_SYS_OUT_OF_LIMIT);
            Assert.Equal(231, (int)AlarmMessages0.IBP1_DIA_OUT_OF_LIMIT);
            Assert.Equal(232, (int)AlarmMessages0.IBP1_MAP_OUT_OF_LIMIT);

            Assert.Equal(240, (int)AlarmMessages0.SPO2_PROBE_DEFECT);
            Assert.Equal(241, (int)AlarmMessages0.SPO2_NO_PROBE);
            Assert.Equal(242, (int)AlarmMessages0.SPO2_PROBE_OFF);
            Assert.Equal(243, (int)AlarmMessages0.SPO2_CHECK_PROBE);
            Assert.Equal(244, (int)AlarmMessages0.SPO2_SEARCH);
            Assert.Equal(245, (int)AlarmMessages0.SPO2_HIGH_AMBIENT_LIGHT);
            Assert.Equal(246, (int)AlarmMessages0.SPO2_SIGNAL_WEAK);

            Assert.Equal(260, (int)AlarmMessages0.SPO2_ASYSTOLE);
            Assert.Equal(261, (int)AlarmMessages0.SPO2_OUT_OF_LIMIT);

            Assert.Equal(280, (int)AlarmMessages0.T1_OUT_OF_LIMIT);
            Assert.Equal(281, (int)AlarmMessages0.T2_OUT_OF_LIMIT);

            Assert.Equal(283, (int)AlarmMessages0.RONT_ARRHYTHMIA);
            Assert.Equal(284, (int)AlarmMessages0.MULTI_PVC_ARRHYTHMIA);
            Assert.Equal(285, (int)AlarmMessages0.PNC_ARRHYTHMIA);
            Assert.Equal(286, (int)AlarmMessages0.PNP_ARRHYTMIA);
        }

    }
}
