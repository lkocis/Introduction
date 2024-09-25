import './AuthorInfo.css';
import React, { useState } from 'react';
import AddAuthorForm from './AddAuthorForm';
import AuthorInfo from './AuthorInfo';
import GetAuthorById from './GetAuthorById';
import UpdateAuthor from './UpdateAuthor';
import { handleDeleteAuthor } from './HandleDeleteAuthor';

function App() {
    const [index, setIndex] = useState(0);
    const [authorsList, setAuthorsList] = useState([]); 
    const [selectedAuthor, setSelectedAuthor] = useState(null); // Changed to null

    return (
        <>
            <h1>Authors List</h1>
            <table className="authors-table">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Date of Birth</th>
                        <th>Image</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {authorsList.length > 0 ? (
                        authorsList.map((author) => (
                            <tr key={author.id}>
                                <td>{author.name}</td>
                                <td>{author.dob}</td>
                                <td>
                                    <img src={author.image} alt={author.name} className="author-table-image" />
                                </td>
                                <td>
                                    <button onClick={() => setSelectedAuthor(author)}>Edit</button>
                                    <button onClick={() => handleDeleteAuthor(index, setIndex, authorsList, setAuthorsList)}>Delete</button>
                                </td>
                            </tr>
                        ))
                    ) : (
                        <tr>
                            <td colSpan="4">No authors available</td>
                        </tr>
                    )}
                </tbody>
            </table>

            <AddAuthorForm authors={authorsList} setList={setAuthorsList} />

            {selectedAuthor && (
                <UpdateAuthor 
                    authors={authorsList} 
                    setList={setAuthorsList} 
                    selectedAuthor={selectedAuthor} 
                    setSelectedAuthor={setSelectedAuthor} 
                />
            )}

            <h3>Find Author</h3>
            <GetAuthorById setSelectedAuthor={setSelectedAuthor} />
        </>
    );
}

export default App;



 


