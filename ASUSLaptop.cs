using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FanControl.Plugins;
using FanControl.ASUSLaptop;
// public AsusControl=new AsusControl();
namespace FanControl.ASUSLaptop
{
   
    public class ASUSLaptop: IPlugin,IDisposable
    {
        public static AsusControl actl;
        public string Name => "ASUS Laptop Fan Plugin";
        public ASUSLaptop() { }
        public void Dispose(){ Close(); }
        public void Initialize() {
            actl = new AsusControl();
        }
        public void Load(IPluginSensorsContainer scon) {
            int fancnt=actl.HealthyTable_FanCounts();
            for(int i=0; i<fancnt; i++)
            {
                scon.FanSensors.Add(new ASUSLaptopSensor(i));
                scon.ControlSensors.Add(new ASUSLaptopControlSensor(i));
            }
        }
        public void Close() {
            int fancnt=actl.HealthyTable_FanCounts();
            for(int i=0; i<fancnt; i++)
            {
                // Give 'em control back
                actl.SetFanSpeed(50, (byte)i, false);
            }
        }
    }
}
