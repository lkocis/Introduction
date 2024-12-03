import React from 'react';
import GetBookingHistory from './GetBookingHistory'; 
import './GetBookingHistory.css';

const bookings = [
    {
        title: "Hotel California",
        dateFrom: "2024-10-01",
        dateTo: "2024-10-05",
        price: "$200",
        state: "Confirmed"
    },
    {
        title: "Grand Hotel",
        dateFrom: "2024-10-06",
        dateTo: "2024-10-10",
        price: "$250",
        state: "Pending"
    },
    {
        title: "Beach Resort",
        dateFrom: "2024-10-11",
        dateTo: "2024-10-15",
        price: "$300",
        state: "Cancelled"
    }
];

function App1() {
    return (
        <div className="bookingHistory">
            <h1>Booking History</h1>
            {bookings.map((booking, index) => (
                <GetBookingHistory
                    key={index}
                    num={index + 1}
                    title={booking.title}
                    DateFrom="Date from:"
                    dateFrom={booking.dateFrom}
                    DateTo="Date to:"
                    dateTo={booking.dateTo}
                    price={booking.price}
                    Price="Total Price:" 
                    state={booking.state}
                />
            ))}
        </div>
    );
}

export default App1;