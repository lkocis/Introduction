import React from 'react';
import './AuthorInfo.css';

function AuthorInfo({ author }) {
  return (
    <div className='authorInfo-container'>
      <h2 className='name-and-DOB'>
        <i>{author.name}</i> 
        <i>({author.dob})</i>
      </h2>

      <img src={author.image} alt={author.name} className='author-image' />
    </div>
  );
}

export default AuthorInfo;