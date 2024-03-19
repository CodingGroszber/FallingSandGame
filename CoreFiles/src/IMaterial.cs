using Raylib_cs;
using System.Collections.Generic;


    public interface IMaterial
    {
        string Name { get; set; }
        Color Color { get; set; }
        float Density { get; set; }
        bool Flammable { get; set; }
        float Viscosity { get; set; }

        void Update();
        void Render();
    }

