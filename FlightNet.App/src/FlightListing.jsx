import { useState } from 'react'
import { getApiData } from './Utils.jsx'


function FlightListing() {
  const [list, setList] = useState([])
  
  const onButtonClick = (e) => {
		const run = async () => {
			const lst = await getApiData("http://localhost:5065/Flights")
			console.log(lst)
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
				</tr>
			</thead>
			<tbody>
				{
					list.map((x, index) => (
						<tr key={index}>
							<td>{x.originCityName}</td>
							<td>{x.destinationCityName}</td>
							<td>{x.planeNameAndNumber}</td>
						</tr>))
				}
			</tbody>			
		</table>
		<div className='d-flex justify-content-end'>
			<button className='btn bg-primary text-white mx-2' onClick={onButtonClick}>
			Get
			</button>
			<button className='btn bg-danger text-white' onClick={onButtonClick}>
			Create
			</button>
		</div>
    </div>
  )
}

export default FlightListing
