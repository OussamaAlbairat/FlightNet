import { useState, useEffect } from 'react'

const None = {},
  Error = {
    theme: "danger",
    icon: "exclamation-triangle",
    message: "Operation failed.",
  },
  Success = {
    theme: "success",
    icon: "hand-thumbs-up",
    message: "Operation succeded.",
  }

const Alert = ({ error, message }) => {
  const [status, setStatus] = useState(None)

  const closeHandler = (e) => {
    setStatus(None)
  }

  useEffect(() => {
    console.log(`error: ${error}`)
    if (error == null) setStatus(None)
    else if (error) setStatus(Error)
    else setStatus(Success)
  }, [error])

  return (
    status == None || (
      <div
        className={`container d-flex justify-content-start alert alert-${status.theme} alert-dismissible fade show`}
        role="alert"
      >
        <i className={`bi bi-${status.icon}`}></i>
        <span className="mx-2">{`${status.message} ${message}`}</span>
        <button
          type="button"
          className="btn-close"
          //data-bs-dismiss="alert"
          aria-label="Close"
          onClick={closeHandler}
        ></button>
      </div>
    )
  )
}

export default Alert
