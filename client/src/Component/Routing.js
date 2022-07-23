import React from 'react';
import {Routes, Route} from 'react-router-dom';
import Actors from './Pages/Actors';
import Home from './Pages/Home';
import Details from './Pages/Details';
function Routing () {
  return (
    <Routes>
      <Route exact path="/" element={<Home />} />
      <Route exact path="/actors" element={<Actors />} /> 
      <Route exact path="/details" element={<Details />} /> 
    </Routes>
  );
}
export default Routing;
