using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.IVH7B4.WebUI.Models {
    public class PincodeViewModel {

        private String pinValue;

        public String PinValue {
            get { return pinValue;}
            set {
                if (value.Length > 4) {
                    pinValue = value.Remove(4);
                }
                else {
                    pinValue = value;
                }
            }
        }
        
        public PincodeViewModel() {
            pinValue = "";
        }
    }
}