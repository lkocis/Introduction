export function handleDeleteAuthor(index, setIndex, authorsList, setAuthorsList) {
    if (index < 0 || index >= authorsList.length) {
        console.error("Invalid index for deletion:", index);
        return;
    }

    const updatedAuthors = [...authorsList.slice(0, index), ...authorsList.slice(index + 1)];

    setAuthorsList(updatedAuthors);
    localStorage.setItem('authors', JSON.stringify(updatedAuthors));

    const newIndex = Math.min(index, updatedAuthors.length - 1); 
    setIndex(newIndex);
}