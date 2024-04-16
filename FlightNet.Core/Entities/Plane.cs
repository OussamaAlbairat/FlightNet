namespace FlightNet.Core.Entities;

public class Plane {

    public int PlaneId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public double AvgCrusingSpeed { get; set;} // unit = km/h
    public double AvgConsumptionAtCrusingAltitude { get; set; } // unit = ton/h
    public double AvgConsumptionOnTakeOff { get; set; } // unit = ton
}