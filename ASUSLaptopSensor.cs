using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FanControl.Plugins;
using FanControl.ASUSLaptop;

namespace FanControl.ASUSLaptop
{
    class ASUSLaptopSensor : IPluginSensor
    {
        public int value = 0;
        public string Id => $"ASUS_FAN_{(int)_fanidx}";
        private byte _fanidx=0;
        public string Name => $"ASUS FAN # {(int)_fanidx+1}";
        public float? Value { get; private set; }
        public ASUSLaptopSensor(int fanId)
        {
            this._fanidx = (byte)fanId; // shold not bigger than sizeof byte
        }
        public void Update()
        {
            this.Value = ASUSLaptop.actl.GetFanSpeed(this._fanidx);
        }
    }
    class ASUSLaptopControlSensor : IPluginControlSensor
    {
        public int value = 0;
        public string Id => $"ASUS_FAN_CTL_{(int)_fanidx}";
        private byte _fanidx=0;
        public string Name => $"ASUS FAN # {(int)_fanidx+1}";
        public float? Value { get; private set; }
        public ASUSLaptopControlSensor(int fanId)
        {
            this._fanidx = (byte)fanId; // shold not bigger than sizeof byte
        }

        public void Update()
        {
            // That's not rpm, it's percent.
            // this.Value = ASUSLaptop.actl.GetFanSpeed(this._fanidx);
        }
        public void Set(float pct)
        {
            this.Value = pct;
            ASUSLaptop.actl.SetFanSpeed((int)pct,_fanidx,true);
        }
        public void Reset()
        {
            ASUSLaptop.actl.SetFanSpeed(50,_fanidx,false);
        }
    }
}
