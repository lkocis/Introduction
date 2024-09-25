import React, { useState, useEffect } from 'react';

function UpdateAuthorForm({ authors, setList, selectedAuthor, setSelectedAuthor }) 
{
    
    const [author, setAuthor] = useState({
        name: "",
        dob: "",
        image: "",
        bio: ""
    });

    useEffect(() => {
        if (selectedAuthor) {
            setAuthor(selectedAuthor);
        }
    }, [selectedAuthor]);

    function handleUpdateAuthor(e)
    {
        const updatedAuthors = authors.map(a => a.id === author.id ? author : a);

        setList(updatedAuthors);
        localStorage.setItem('authors', JSON.stringify(updatedAuthors));

        setAuthor({
            name: "",
            dob: "",
            image: "",
            bio: ""
        });

    setSelectedAuthor(null);
    }
    

    

    function handleChange(e)
    {
        setAuthor({...author, [e.target.name] : e.target.value})
    }


    return (
        <div>
            <h3>Update Author</h3>
            <label>
                Id: 
                <input
                    type = "text"
                    name ="id"
                    value = {author.Id}
                    onChange  = {handleChange}
                />
            </label>
            <br />
            <label>
                Name:
                <input
                    type="text"
                    name="name"
                    value={author.name}
                    onChange={handleChange}
                />
            </label>
            <br />
            <label>
                Date Of Birth:
                <input
                    type="text"
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
            <label>
                Bio:
                <textarea
                    name="bio"
                    value={author.bio}
                    onChange={handleChange}
                />
            </label>
            <br />
            <button onClick={handleUpdateAuthor}>Update</button>
        </div>
    );
}

export default UpdateAuthorForm;

