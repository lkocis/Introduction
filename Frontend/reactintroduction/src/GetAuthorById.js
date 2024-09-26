import React, { useState } from 'react';
import './AuthorInfo.css';
import axios from 'axios';

function GetAuthorById({ setSelectedAuthor }) {
  const [id, setId] = useState("");
  const [authorInfo, setAuthorInfo] = useState(null); 
  const [error, setError] = useState("");

  function handleChange(e) {
    setId(e.target.value);
  }

  function handleGetAuthor(e) {
    e.preventDefault();
    axios.get(`https://localhost:7042/author/getAuthorbyid/${id}`)
      .then((response) => {
        const foundAuthor = response.data;
        setSelectedAuthor(foundAuthor);
        setAuthorInfo(foundAuthor); 
        setError(""); 
      })
      .catch((err) => {
        console.error("Error fetching author:", err);
        setError("Author not found!"); 
        setAuthorInfo(null); 
      });
      setId("");
    }
  return (
    <>
      <form onSubmit={handleGetAuthor}>
        <label>
          Enter ID:
          <input type="text" value={id} onChange={handleChange} required />
        </label>
        <button type="submit">Search Author</button>
      </form>

      <br/>
      {authorInfo && (
        <table className='author-table'>
          <thead>
            <tr>
              <th>FirstName</th>
              <th>LastName</th>
              <th>Date of Birth</th>
              <th>Image</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td>{authorInfo.firstName}</td>
              <td>{authorInfo.lastName}</td>
              <td>{authorInfo.dob}</td>
              <td>
                <img src={authorInfo.image} alt={authorInfo.firstName} style={{ width: '100px' }} />
              </td>
            </tr>
          </tbody>
        </table>
      )}
    </>
  );
}

export default GetAuthorById;