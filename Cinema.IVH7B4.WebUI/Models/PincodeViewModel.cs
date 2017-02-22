/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.IVH7B4.WebUI.Models {
    public class PincodeViewModel {
        public string MovieName { get; set; }
        public decimal MoviePrice { get; set; }
        public string WrongPin { get; set; }
        private String pinValue;

        public String PinValue {
            get { return pinValue; }
            set {
                if (value.Length > 4) {
                    pinValue = value.Remove(4);
                }
                if (pinValue == "0000") {
                    WrongPin = "Voer een geldige pincode in";
                    pinValue = "";
                } 
                else {
                    pinValue = value;
                }
            }
        }
    }
}
*/
