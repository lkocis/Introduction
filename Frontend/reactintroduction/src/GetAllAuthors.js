import React, { useEffect } from 'react';
import axios from 'axios';

function GetAllAuthors({ setAuthorsList }) {
    useEffect(() => {
        const fetchAuthors = async () => {
            try {
                const response = await axios.get('http://localhost:7042/Author/getall');
                setAuthorsList(response.data);  
            } catch (error) {
                console.error("Error fetching authors:", error);
            }
        };

        fetchAuthors();
    }, [setAuthorsList]);  

    return null;  
}

export default GetAllAuthors;