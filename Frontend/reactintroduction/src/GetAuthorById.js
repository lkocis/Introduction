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

    axios.get(`http://localhost:7042/Author/GetAuthorById/{id}`)
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

    /*const authors = JSON.parse(localStorage.getItem('authors')) || [];
    const authorId = parseInt(id);
    const foundAuthor = authors.find(a => a.id === authorId);
    if (foundAuthor) {
      setSelectedAuthor(foundAuthor); // Pass to parent if needed
      setAuthorInfo(foundAuthor); // Set the state to show in the table
    } else {
      alert('Author not found!');
      setAuthorInfo(null); // Clear the table if no author is found
    }
    setId("");*/
  }

  return (
    <>
      <form onSubmit={handleGetAuthor}>
        <label>
          Enter ID:
          <input type="number" value={id} onChange={handleChange} required />
        </label>
        <button type="submit">Search Author</button>
      </form>

      <br/>
      {/* Display author info in a table if found */}
      {authorInfo && (
        <table className='author-table'>
          <thead>
            <tr>
              <th>Name</th>
              <th>Date of Birth</th>
              <th>Image</th>
              <th>Bio</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td>{authorInfo.name}</td>
              <td>{authorInfo.dob}</td>
              <td>
                <img src={authorInfo.image} alt={authorInfo.name} style={{ width: '100px' }} />
              </td>
              <td>{authorInfo.bio}</td>
            </tr>
          </tbody>
        </table>
      )}
    </>
  );
}

export default GetAuthorById;