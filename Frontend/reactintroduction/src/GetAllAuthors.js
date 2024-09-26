import React, { useEffect } from 'react';
import axios from 'axios';

function GetAllAuthors({ setAuthorsList }) {
    useEffect(() => {
        axios.get('http://localhost:7042/Author/getall') 
            .then(response => {
                setAuthorsList(response.data); 
            })
            .catch(error => {
                console.error("Error fetching authors:", error);
            });
    }, [setAuthorsList]); 

    return null; 
}

export default GetAllAuthors;