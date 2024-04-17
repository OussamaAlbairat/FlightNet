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
    console.log(data)
    let result = {status:"OK", message: "OK"}
    const headers = new Headers()
    headers.append("Content-Type", "application/json")
    const options = { method: "POST", headers: headers, body: JSON.stringify(data) }
    try {
        const resp = await fetch(uri, options)
        result = (resp.ok) ? { status:"OK", message: "Create!"} 
                                 : { status:"NOK", message: await resp.text()}
    
    }
    catch(error) {
        result = { status:"NOK", message: error}
        console.log(error)
    }
    return result
}

export const putApiData = async (uri, data) => {
    console.log(data)
    let result = {status:"OK", message: "OK"}
    const headers = new Headers()
    headers.append("Content-Type", "application/json")
    const options = { method: "PUT", headers: headers, body: JSON.stringify(data) }
    try {
        const resp = await fetch(uri, options)
        result = (resp.ok) ? { status:"OK", message: "Update!"} 
                                 : { status:"NOK", message: await resp.text()}
    
    }
    catch(error) {
        result = { status:"NOK", message: error}
        console.log(error)
    }
    return result
}

export const deleteApiData = async (uri) => {
    const options = { method: "DELETE" }
    let result = {status:"OK", message: "OK"}
    try {
        const resp = await fetch(uri, options)
        result = (resp.ok) ? { status:"OK", message: "Delete!"} 
                                 : { status:"NOK", message: await resp.text()}
    
    }
    catch(error) {
        result = { status:"NOK", message: error}
        console.log(error)
    }
    return result
}