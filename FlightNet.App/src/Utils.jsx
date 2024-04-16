export const getApiData = async (uri) => {
    const options = { method: "GET" }
    const resp = uri ? await fetch(uri, options) : null
    const dt = resp ? await resp.json() : []
    return dt
}

export const postApiData = async (uri, data) => {
    const options = { method: "POST", body: JSON.stringify(data) }
    const resp = uri ? await fetch(uri, options) : null
    const dt = resp ? await resp.json() : []
    return dt
}