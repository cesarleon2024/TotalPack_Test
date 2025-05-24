/*
import { environment } from '../environments/environment';
const apiUrl = environment.API_GetUsers;
*/

export const environment = {
    production: false,

    API_GetUsers: 'https://localhost:7073/api/User',
    API_GetUserById: 'https://localhost:7073/api/User',
    API_AddUser: 'https://localhost:7073/api/User',
    API_EditUser: 'https://localhost:7073/api/User',
    API_DeleteUser: 'https://localhost:7073/api/User'
};