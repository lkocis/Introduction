import React, { useState } from 'react';
import './AuthorInfo';
import axios from 'axios';


function AddAuthorForm({ authors, setList }) {
    const [author, setAuthor] = useState({
        firstName: "",
        lastName: "",
        dob: "",
        image: ""
    });

    function handleChange(e)
    {
      setAuthor({...author, [e.target.name] : e.target.value})
    }

    debugger
    function handleAddAuthor(e) 
    {
      e.preventDefault();
      const formattedDate = new Date(author.dob).toISOString();
      const newAuthor = {...author, dob: formattedDate};

      axios.post(`https://localhost:7042/author/postauthor`, newAuthor)
      .then((response)=> {
        console.log("Response from server:", response.data);
        const addedAuthor = response.data; 
        const updatedAuthors = [...authors, addedAuthor];
        setList(updatedAuthors);

        setAuthor({
            firstName: "",
            lastName: "",
            dob: "",
            image: ""
        });
      })
      .catch((error) => {
        console.error("There was an error adding the author!", error);
      });
    }

  return (
    <form onSubmit={handleAddAuthor}> 
            <h3>Add Author</h3>
            <label>
                FirstName:
                <input 
                    type="text" 
                    name="firstName" 
                    value={author.firstName} 
                    onChange={handleChange} 
                    required 
                />
            </label>
            <label>
                LastName:
                <input 
                    type="text" 
                    name="lastName"
                    value={author.lastName} 
                    onChange={handleChange} 
                    required 
                />
            </label>
            <label>
                Date of Birth:
                <input 
                    type="date" 
                    name="dob" 
                    value={author.dob} 
                    onChange={handleChange} 
                    required 
                />
            </label>
            <label>
                Image URL:
                <input 
                    type="text" 
                    name="image" 
                    value={author.image} 
                    onChange={handleChange} 
                    required 
                />
            </label>
            <button type="submit">Add</button> 
        </form>
  );
}

export default AddAuthorForm;