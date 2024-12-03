import React, { useState, useEffect } from 'react';
import axios from 'axios';

function UpdateAuthorForm({ authors, setList, selectedAuthor, setSelectedAuthor }) 
{
    
    const [author, setAuthor] = useState({
        firstName: "",
        lastName: "",
        dob: "",
        image: ""
    });

    useEffect(() => {
        if (selectedAuthor) {
            setAuthor(selectedAuthor);
        }
    }, [selectedAuthor]);

    function handleUpdateAuthor(e) {
        e.preventDefault(); 

        axios.put(`http://localhost:7042/author/putauthorbyid/${author.id}`, author)
            .then((response) => {
                const updatedAuthors = authors.map(a => a.id === author.id ? response.data : a);
                setList(updatedAuthors);
            })
            .catch((error) => {
                console.error("There was an error updating the author!", error);
            });

        setAuthor({
            firstName: "",
            lastName: "",
            dob: "",
            image: ""
        });

        setSelectedAuthor(null);
    }

    function handleChange(e) {
        setAuthor({ ...author, [e.target.name]: e.target.value });
    }

    return (
        <div>
            <form onSubmit={handleUpdateAuthor}>
                <h3>Update author</h3>
                <label>
                    First Name:
                    <input
                        type="text"
                        name="firstName" 
                        value={author.firstName}
                        onChange={handleChange}
                    />
                </label>
                <br />
                <label>
                    Last Name:
                    <input
                        type="text"
                        name="lastName" 
                        value={author.lastName}
                        onChange={handleChange}
                    />
                </label>
                <br />
                <label>
                    Date Of Birth:
                    <input
                        type="date"
                        name="dob"
                        value={author.dob}
                        onChange={handleChange}
                    />
                </label>
                <br />
                <label>
                    Image URL:
                    <input
                        type="text"
                        name="image"
                        value={author.image}
                        onChange={handleChange}
                    />
                </label>
                <br />
                <button type="submit">Update</button>
            </form>
        </div>
    );
}

export default UpdateAuthorForm;

