import axios from 'axios';
import { useEffect, useState } from 'react';

function Authors() {
    const [authors, setAuthors] = useState([]);

    // Fetch authors from backend
    useEffect(() => {
        axios.get('http://localhost:5000/api/authors')
            .then(response => {
                setAuthors(response.data);
            })
            .catch(error => {
                console.error('Error fetching authors:', error);
            });
    }, []);

    const addAuthor = (newAuthor) => {
        axios.post('http://localhost:5000/api/authors', newAuthor)
            .then(() => {
                setAuthors([...authors, newAuthor]);
            })
            .catch(error => {
                console.error('Error adding author:', error);
            });
    };

    return (
        <div>
            <ul>
                {authors.map((author, index) => (
                    <li key={index}>{author.name}</li>
                ))}
            </ul>
            <button onClick={() => addAuthor({ name: 'New Author' })}>Add Author</button>
        </div>
    );
}

export default Authors;