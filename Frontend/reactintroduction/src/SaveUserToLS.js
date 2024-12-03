function SaveUserToLS(user) {
    const userId = user.id;  
    localStorage.setItem(`user_${userId}`, JSON.stringify(user)); 
}

export { SaveUserToLS }; 

const users = [
    {
        id: 23456,
        firstName: 'Ana',
        lastName: 'Anic',
        email: 'ana@example.com',
        phoneNumber: '123-456-7890'
    },
    {
        id: 34567,
        firstName: 'Pero',
        lastName: 'Peric',
        email: 'pero@example.com',
        phoneNumber: '123-456-7890'
    }
];

export default users;  