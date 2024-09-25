import React, { useState } from 'react';
import './App.css';
import axios from 'axios';


function AddAuthorForm({ authors, setList }) {
    const [author, setAuthor] = useState({
        name: "",
        dob: "",
        image: "",
        bio: ""
    });

    function handleChange(e)
    {
      setAuthor({...author, [e.target.name] : e.target.value})
    }

    function handleAddAuthor() 
    {
      axios.post('http://localhost:7042/Author/PostAuthor')
      .then()
      const newAuthor = { ...author, id: authors.length + 1 };
  
      const updatedAuthors = [...authors, newAuthor];
  
      setList(updatedAuthors);
  
      localStorage.setItem('authors', JSON.stringify(updatedAuthors));
  
      // Clear the form
      setAuthor({
        name: "",
        dob: "",
        image: "",
        bio: ""
      });
    }

  return (
    <form>
    <>
    <h3>Add Author</h3>
            <label>
                Name:
                <input type="text" name="name" value={author.name} onChange={handleChange} required />
            </label>
            <label>
                Date of Birth:
                <input type="text" name="dob" value={author.dob} onChange={handleChange} required />
            </label>
            <label>
                Image URL:
                <input type="text" name="image" value={author.image} onChange={handleChange} required />
            </label>
            <button onClick={handleAddAuthor}>Add</button>
    </>
    </form>
  );
}

export default AddAuthorForm;