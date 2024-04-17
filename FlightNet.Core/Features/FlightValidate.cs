using FlightNet.Core.Contracts;
using FlightNet.Core.Entities;

namespace FlightNet.Core.Features;

public class FlightValidate { 

    public void Assert(Flight flight) {
        if (flight is null) 
            throw new ValidationException("Flight is empty");
        if (flight.Plane is null) 
            throw new ValidationException("Flight Plane is not valid");
        if (flight.Origin is null) 
            throw new ValidationException("Flight Origin is not valid");
        if (flight.Destination is null) 
            throw new ValidationException("Flight Destination is not valid");
    }
}