import axios from 'axios';
import {BasicURL} from './Environment';
export async function handleGetMoviesList () {
  let res = null;
  const options = {
    method: 'GET',
    url: `${BasicURL}/api/Movie`,
  };
  await axios
    .request (options)
    .then (function (response) {
      console.log ('res..', response.data);
      res = response.data;
    })
    .catch (function (error) {
      res = error;
      console.error ('error..', error);
    });
  return res;
}
export async function handleGetMoviesDetails (id) {
  let res = null;
  const options = {
    method: 'GET',
    url: `${BasicURL}/api/Movie/${id}`,
  };
  await axios
    .request (options)
    .then (function (response) {
      console.log ('res..', response.data);
      res = response.data;
    })
    .catch (function (error) {
      res = error;
      console.error ('error..', error);
    });
  return res;
}
export async function handleGetAllActor () {
  let res = null;
  const options = {
    method: 'GET',
    url: `${BasicURL}/api/Person`,
  };
  await axios
    .request (options)
    .then (function (response) {
      console.log ('res..', response.data);
      localStorage.setItem ('Actor', JSON.stringify (response.data));
      res = response.data;
    })
    .catch (function (error) {
      res = error;
      console.error ('error..', error);
    });
  return res;
}
