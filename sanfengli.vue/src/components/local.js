export default {
    setItem: (name, value) => {
        if (typeof value === 'object') {
            value = JSON.stringify(value);
        }

        localStorage.setItem(name, value);
    },
    getItem: name => {
        let value = localStorage.getItem(name);
        try {
            return JSON.parse(value);
        } catch (error) {
            return value;
        }
    },
    removeItem: name => localStorage.removeItem(name),

    logout: () => {
        localStorage.removeItem('token');
        localStorage.removeItem('user');
    }

};