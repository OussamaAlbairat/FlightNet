import {
  createBrowserRouter,
  createRoutesFromElements,
  RouterProvider,
  Route,
} from "react-router-dom"

import FlightListing  from './FlightListing.jsx'
import FlightDetails from './FlightDetails.jsx'

const router = createBrowserRouter(
  createRoutesFromElements(
    <Route path="/">
      <Route index element={<FlightListing />} />
      <Route path="details" element={<FlightDetails />} />
      <Route path="details/:id" element={<FlightDetails />} />
    </Route>
  )
)

function App() {
  return <RouterProvider router={router} />
}

export default App
