import axios from 'axios';
import { API_HOST_PREFIX, onGlobalError, onGlobalSuccess } from './serviceHelpers';

const route = '/api/sitereferences';
const API = API_HOST_PREFIX + route;

let addUserReference = (payload) => {
    const config = {
        method: 'POST',
        url: API,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };

    return axios(config);
};

let getAll = (pageIndex, pageSize) => {
    const config = {
        method: 'GET',
        url: API + `/paginate?pageIndex=${pageIndex}&pageSize=${pageSize}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};

let getAllTypes = () => {
    const config = {
        method: 'GET',
        url: API + `/types`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };

    return axios(config);
};
let getAllForChart = () => {
    const config = {
        method: 'GET',
        url: API + `/chart/data`, ///str interp
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };

    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const siteReferenceService = {
    getAll,
    addUserReference,
    getAllTypes,
    getAllForChart,
};
export default siteReferenceService;
