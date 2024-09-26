import './AuthorInfo.css';
import React, { useState } from 'react';
import AddAuthorForm from './AddAuthorForm';
import GetAuthorById from './GetAuthorById';
import UpdateAuthor from './UpdateAuthor';
import { handleDeleteAuthor } from './HandleDeleteAuthor';
import GetAllAuthors from './GetAllAuthors';

function App() {
    const [index, setIndex] = useState(0);
    const [authorsList, setAuthorsList] = useState([]); 
    const [selectedAuthor, setSelectedAuthor] = useState(null); 

    return (
        <>
            <h1>Authors List</h1>
            <table className="authors-table">
                <thead>
                    <tr>
                        <th>FirstName</th>
                        <th>LastName</th>
                        <th>Date of Birth</th>
                        <th>Image</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {authorsList.map((author) => (
                            <tr key={author.id}>
                                <td>{author.firstName}</td>
                                <td>{author.lastName}</td>
                                <td>{author.dob}</td>
                                <td>
                                    <img src={author.image} alt={author.firstName} className="author-table-image" />
                                </td>
                                <td>
                                    <button onClick={() => setSelectedAuthor(author)}>Edit</button>
                                    <button onClick={() => handleDeleteAuthor(index, setIndex, authorsList, setAuthorsList)}>Delete</button>
                                </td>
                            </tr>
                        ))
                    }
                </tbody>
            </table>

            <AddAuthorForm authors={authorsList} setList={setAuthorsList} />
            <GetAllAuthors setAuthorsList={setAuthorsList}/>

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



 


