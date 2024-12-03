import React, { useState, useEffect } from 'react';

const UserInfo = ({ userId }) => {
  const [user, setUser] = useState(null);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchUserInfoFromLocalStorage = () => {
      try {
        const storedUser = localStorage.getItem(`user_${userId}`);
        if (!storedUser) {
          throw new Error('User not found');
        }
        const userData = JSON.parse(storedUser);
        setUser(userData);
      } catch (err) {
        setError(err.message);
      }
    };

    if (userId) {
      fetchUserInfoFromLocalStorage();
    }
  }, [userId]);

  if (error) {
    return <div>Error: {error}</div>;
  }

  if (!user) {
    return <div>No user found</div>;
  }

  return (
    <div>
      <p><strong>First Name:</strong> {user.firstName}</p>
      <p><strong>Last Name:</strong> {user.lastName}</p>
      <p><strong>Email:</strong> {user.email}</p>
      <p><strong>Phone Number:</strong> {user.phoneNumber}</p>
    </div>
  );
};

export default UserInfo;