const dev = {
    BASE_URL:"http://localhost:5065/"
}

const prod = {
    BASE_URL:"https://flightnet.azurewebsites.net/"
}

export const Config = process.env.NODE_ENV === 'development' ? dev : prod;