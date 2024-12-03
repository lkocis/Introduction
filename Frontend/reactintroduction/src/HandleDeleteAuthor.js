import axios from 'axios';

async function handleDeleteAuthor(index, setIndex, authorsList, setAuthorsList) {
    const authorToDelete = authorsList[index];

    try {
        await axios.delete(`http://localhost:7042/author/deleteauthorbyid/${authorToDelete.id}`);

        const updatedAuthors = [...authorsList.slice(0, index), ...authorsList.slice(index + 1)];
        setAuthorsList(updatedAuthors);

        const newIndex = Math.min(index, updatedAuthors.length - 1);
        setIndex(newIndex);
    } catch (error) {
        console.error("Error deleting author:", error);
        alert("Failed to delete the author. Please try again.");
    }
}

export default handleDeleteAuthor;