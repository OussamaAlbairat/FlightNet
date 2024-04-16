import { useState, useEffect } from 'react'
import { getApiData } from './Utils.jsx'


function FlightDetails() {

  const [flight, setFlight] = useState([{
    originCityId: null,
    destinationCityId: null,
    planeId: null
  }])
  
  const [cities, setCities] = useState([])

  const [planes, setPlanes] = useState([])

  useEffect(()=> {
    const initialize = async () => {
        const cts = await getApiData("http://localhost:5065/Cities")
        console.log(cts)
        setCities(cts)

        const pls = await getApiData("http://localhost:5065/Planes")
        console.log(pls)
        setPlanes(pls)

        const flt = await getApiData("http://localhost:5065/Flights/1")
        console.log(flt)
        setFlight(flt)
    }
    initialize()
  }, [])

  const onOriginCityChanged = (e) => {
    setFlight((old)=> {
        return [{...old[0], originCityId:e.target.value}]
    })
  }

  const onDestinationCityChanged = (e) => {
    setFlight((old)=> {
        return [{...old[0], destinationCityId:e.target.value}]
    })
  }

  const onPlaneChanged = (e) => {
    setFlight((old)=> {
        return [{...old[0], planeId:e.target.value}]
    })
  }

  const onSubmitClick = (e) => {
		const run = async () => {
			await postApiData("http://localhost:5065/Flights", flight)
		}
		run()
  }

  const onCancelClick = (e) => {
  }

  return (
    <div className='container'>
		<h3 className='text-left'>Flight</h3>
        <div className='row'>
            <div className='col-8'>
                <div className="mb-3">
                    <label htmlFor="originCityName" className="form-label">Origin</label>
                    <select id="originCityName" className="form-select" onChange={onOriginCityChanged}>
                        {cities.map((x, index)=>{
                            if (flight.length && flight[0].originCityId == x.cityId) {
                                return (<option key={index} value={x.cityId} selected>{x.cityName}</option>)    
                            }
                            else
                                return (<option key={index} value={x.cityId}>{x.cityName}</option>)
                        })}
                    </select>
                </div>
                <div className="mb-3">
                    <label htmlFor="destinationCityName" className="form-label">Destination</label>
                    <select id="destinationCityName" className="form-select" onChange={onDestinationCityChanged}>
                        {cities.map((x, index)=>{
                            if (flight.length && flight[0].destinationCityId == x.cityId) {
                                return (<option key={index} value={x.cityId} selected>{x.cityName}</option>)    
                            }
                            else
                                return (<option key={index} value={x.cityId}>{x.cityName}</option>)
                        })}
                    </select>
                </div>
                <div className="mb-3">
                    <label htmlFor="planeName" className="form-label">Plane</label>
                    <select id="planeName" className="form-select" onChange={onPlaneChanged}>
                        {planes.map((x, index)=>{
                            if (flight.length && flight[0].planeId == x.planeId) {
                                return (<option key={index} value={x.planeId} selected>{x.planeNameAndNumber}</option>)
                            }
                            else
                                return (<option key={index} value={x.planeId}>{x.planeNameAndNumber}</option>)
                        })}
                    </select>
                </div>
            </div>
            <div className='col-4'>
                <div className="mb-3">
                    <label htmlFor="consumption" className="form-label">Consumption</label>
                    <input id="consumption" type='text' class="form-control" value={flight[0].consumption} disabled />
                </div>
                <div className="mb-3">
                    <label htmlFor="distance" className="form-label">Distance</label>
                    <input id="distance" type='text' class="form-control" value={flight[0].distance} disabled/>
                </div>
            </div>
        </div>
        <div className='row'>
            <div className='col-12 d-flex justify-content-end'>
                <button className='btn bg-primary text-white mx-2' onClick={onCancelClick}>
                Cancel
                </button>
                <button className='btn bg-danger text-white' onClick={onSubmitClick}>
                Submit
                </button>
            </div>
        </div>
    </div>
  )
}

export default FlightDetails
