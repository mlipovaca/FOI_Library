﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceModule
{
    public interface IPrijava
    {
        bool StanjeZadnjePrijave { get; set; }
        string UneseniPodatak { get; set; }
        void PrijavaModulom(Action<Type,Action<Type>,string> otvaranjeUI, Action<Type> zatvaranjeUI, string HashiraniPodatak);             
        void PromjenaPodataka(Action<Type,Action<Type>,string, Action<string>> otvaranjeUI, Action<Type> zatvaranjeUI, string HashiraniPodatak);
        void VratiUneseniPodatak(string proslijedeniPodatak);
    }
}
