using FanControl.ASUSLaptop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanControl.ASUSLaptop
{
    public class AsusControl
    {
        public AsusControl()
        {
            AsusWinIO64.InitializeWinIo();
        }

        ~AsusControl()
        {
            AsusWinIO64.ShutdownWinIo();
        }

        public void SetFanSpeed(byte value, byte fanIndex = 0, bool con=false)
        {
            AsusWinIO64.HealthyTable_SetFanIndex(fanIndex);
            AsusWinIO64.HealthyTable_SetFanPwmDuty(value);
            // If force con || val >0 then test ON
            // else test off
            AsusWinIO64.HealthyTable_SetFanTestMode((char)((con || value>0)? 0x01 : 0x00));
        }

        public void SetFanSpeed(int percent, byte fanIndex = 0,bool con=false)
        {
            var value = (byte)(percent / 100.0f * 255);
            SetFanSpeed(value, fanIndex,con);
        }

        public void SetFanSpeeds(byte value)
        {
            var fanCount = AsusWinIO64.HealthyTable_FanCounts();
            for(byte fanIndex = 0; fanIndex < fanCount; fanIndex++)
            {
                SetFanSpeed(value, fanIndex);
            }
        }

        public void SetFanSpeeds(int percent)
        {
            var value = (byte)(percent / 100.0f * 255);
            SetFanSpeeds(value);
        }

        public int GetFanSpeed(byte fanIndex = 0)
        {
            AsusWinIO64.HealthyTable_SetFanIndex(fanIndex);
            var fanSpeed = AsusWinIO64.HealthyTable_FanRPM();
            return fanSpeed;
        }

        public List<int> GetFanSpeeds()
        {
            var fanSpeeds = new List<int>();

            var fanCount = AsusWinIO64.HealthyTable_FanCounts();
            for (byte fanIndex = 0; fanIndex < fanCount; fanIndex++)
            {
                var fanSpeed = GetFanSpeed(fanIndex);
                fanSpeeds.Add(fanSpeed);
            }

            return fanSpeeds;
        }

        public int HealthyTable_FanCounts()
        {
            return AsusWinIO64.HealthyTable_FanCounts();
        }

        public ulong Thermal_Read_Cpu_Temperature()
        {
            return AsusWinIO64.Thermal_Read_Cpu_Temperature();
        }
    }
}
