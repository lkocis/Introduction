import React from "react"
import './GetBookingHistory.css';

function GetBookingHistory ({num, price, Price, dateFrom, DateFrom, DateTo,  dateTo, state, title, Title})
{
    return(
        <div className="bookingHistory-container">
            <h2>Reservation {num}</h2>
            <div className="description">
                <p><span>{Title}</span>{title}</p>
                <p><span>{DateFrom}</span> {dateFrom}</p>
                <p><span>{DateTo}</span> {dateTo}</p>
                <p><span>{Price}</span> {price}</p>
            </div>
            <div className="state">{state}</div>
            <button className="addReview-button">Add review</button>
        </div>
    )

}

export default GetBookingHistory;