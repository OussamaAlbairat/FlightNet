import { Config } from './Config.js'
import { useState, useEffect } from 'react'
import { useParams } from "react-router-dom"
import Alert from './Alert.jsx'
import { getApiData, postApiData, putApiData, financial, isInteger } from './Utils.jsx'


function FlightDetails() {

  const { id } = useParams()

  const [id_, setId_] = useState(id)

  const [refresh, setRefresh] = useState(0)

  const [error, setError] = useState(null)
  const [message, setMessage] = useState(null)

  const [flight, setFlight] = useState([{
    flightId:0,
    originCityId: 0,
    destinationCityId: 0,
    planeId: 0,
    consumption: 0.0,
    distance: 0.0
  }])
  
  const [cities, setCities] = useState([])

  const [planes, setPlanes] = useState([])

  useEffect(()=> {
    const initialize = async () => {
        const cts = await getApiData(Config.BASE_URL + "Cities")
        setCities(cts)

        const pls = await getApiData(Config.BASE_URL + "Planes")
        setPlanes(pls)
    }
    initialize()
  }, [])

  useEffect(()=> {
    const initialize = async () => {
        const url = isInteger(id_) ? `${Config.BASE_URL}Flights/${id_}`:null
        const flt = url ? await getApiData(url) : [{
            flightId:0,
            originCityId: 0,
            destinationCityId: 0,
            planeId: 0,
            consumption: 0.0,
            distance: 0.0
          }]
        setFlight(flt)
    }
    initialize()
  }, [id_, refresh])

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
            const result = 
                id_ ? await putApiData(Config.BASE_URL + "Flights", 
                    {
                        flightId:flight[0].flightId,
                        originCityId: flight[0].originCityId,
                        destinationCityId: flight[0].destinationCityId,
                        planeId: flight[0].planeId
                    })
                    : await postApiData(Config.BASE_URL + "Flights", 
                    {
                        originCityId: flight[0].originCityId,
                        destinationCityId: flight[0].destinationCityId,
                        planeId: flight[0].planeId
                    })
            if (result.status == "OK" && result.data && result.data.flightId) {
                setId_(result.data.flightId)
            }
            else if(result.status == "OK") setRefresh(refresh + 1)
            setError(result.status!="OK")
            setMessage(result.message)
            console.log(result)
		}
		run()
  }

  return (
    <div className='container'>
		<h3 className='text-left'>Flight</h3>
        <Alert error={error} message={message} setError={setError}/>
        <div className='row'>
            <div className='col-8'>
                <div className="mb-3">
                    <label htmlFor="originCityName" className="form-label">Origin</label>
                    <select id="originCityName" className="form-select" onChange={onOriginCityChanged}>
                        {(!flight.length || flight[0].originCityId == null)? 
                            (<option value={0} selected>Select city</option>)    
                            :(<option value={0}>Select city</option>)}
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
                        {(!flight.length || flight[0].destinationCityId == null)? 
                            (<option value={0} selected>Select city</option>)    
                            :(<option value={0}>Select city</option>)}
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
                        {(!flight.length || flight[0].planeId == null)? 
                                (<option value={0} selected>Select plane</option>)    
                                :(<option value={0}>Select plane</option>)}
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
                    <label htmlFor="consumption" className="form-label">Consumption (tons)</label>
                    <input id="consumption" type='text' className="form-control" value={financial(flight[0].consumption)} disabled />
                </div>
                <div className="mb-3">
                    <label htmlFor="distance" className="form-label">Distance (km)</label>
                    <input id="distance" type='text' className="form-control" value={financial(flight[0].distance)} disabled/>
                </div>
            </div>
        </div>
        <div className='row'>
            <div className='col-12 d-flex justify-content-end'>
                <a href="/" className="btn bg-primary text-white mx-2" role="button" aria-pressed="true">
                    Cancel
                </a>
                <button className='btn bg-danger text-white' onClick={onSubmitClick}>
                    Submit
                </button>
            </div>
        </div>
    </div>
  )
}

export default FlightDetails
