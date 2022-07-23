import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import {ContextProvider} from './Component/Context';
const root = ReactDOM.createRoot (document.getElementById ('root'));
root.render (
  <ContextProvider>
    <App />
  </ContextProvider>
);

// If you want to start measuring performance in your app, pass a function
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals ();
