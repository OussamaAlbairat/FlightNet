import { Config } from './Config.js'
import { useState } from 'react'
import { getApiData, deleteApiData } from './Utils.jsx'


function FlightListing() {
  const [list, setList] = useState([])
  
  const onRefreshClick = (e) => {
		const run = async () => {
			const lst = await getApiData(Config.BASE_URL + "Flights")
			console.log(lst)
			setList(lst)
		}
		run()
  }

  const deleteFlight = (url) => {
	const run = async () => {
		if (!confirm("Do you want to delete this flight?")) return
		await deleteApiData(url)
		const lst = await getApiData(Config.BASE_URL + "Flights")
		setList(lst)
	}
	run()
  }

  return (
    <div className='container'>
		<h3 className='text-left'>Flights</h3>
		<table className='table'>
			<thead>
				<tr className='bg-primary text-white'>
					<th>Origin</th>
					<th>Destination</th>
					<th>Plane</th>
					<th></th>
					<th></th>
				</tr>
			</thead>
			<tbody>
				{
					list.map((x, index) => (
						<tr key={index}>
							<td>{x.originCityName}</td>
							<td>{x.destinationCityName}</td>
							<td>{x.planeNameAndNumber}</td>
							<td>
								<a href={`details/${x.flightId}`}>
									<i className="bi bi-pencil-square"></i>
								</a>
							</td>
							<td>
								<a href="#" onClick={(e)=> { deleteFlight(`http://localhost:5065/Flights?id=${x.flightId}`) }}>
									<i className="bi bi-file-x"></i>
								</a>
							</td>
						</tr>))
				}
			</tbody>			
		</table>
		<div className='d-flex justify-content-end'>
			<button className='btn bg-primary text-white mx-2' onClick={onRefreshClick}>
				Refresh
			</button>
			<a href="details" className="btn btn-danger " role="button" aria-pressed="true">
				Create
            </a>
		</div>
    </div>
  )
}

export default FlightListing
