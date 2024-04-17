export const financial = (x) => {
    const value = Number.parseFloat(x)
    if (Number.isNaN(value)) return ''
    else return value.toFixed(2)
} 

export const isInteger = (a) => !Number.isNaN(Number.parseInt(a))

export const getApiData = async (uri) => {
    const options = { method: "GET" }
    const resp = uri ? await fetch(uri, options) : null
    const dt = resp ? await resp.json() : []
    return dt
}

export const postApiData = async (uri, data) => {
    const headers = new Headers()
    headers.append("Content-Type", "application/json")
    const options = { method: "POST", headers: headers, body: JSON.stringify(data) }
    const resp = uri ? await fetch(uri, options) : null
    const dt = resp ? await resp.json() : []
    return dt
}

export const putApiData = async (uri, data) => {
    const headers = new Headers()
    headers.append("Content-Type", "application/json")
    const options = { method: "PUT", headers: headers, body: JSON.stringify(data) }
    const resp = uri ? await fetch(uri, options) : null
    const dt = resp ? await resp.json() : []
    return dt
}

export const deleteApiData = async (uri) => {
    const options = { method: "DELETE" }
    const resp = uri ? await fetch(uri, options) : null
    const dt = resp ? await resp.json() : []
    return dt
}