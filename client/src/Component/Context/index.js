import React, {createContext, useState} from 'react';
export const Context = createContext ();
export const ContextProvider = ({children}) => {
  const [DisplaySignUp, setDisplaySignUp] = useState (false);
  const [DisplayLogIn, setDisplayLogIn] = useState (false);
  const [UpdateDetailPageId, setUpdateDetailPageId] = useState(false)
  function HandleDisplaySignUp (toggle) {
    setDisplaySignUp (toggle);
  }
  function HandleDisplayLogin (toggle) {
    setDisplayLogIn (toggle);
  }
  function HandleUpdateDetailPageId (toggle) {
    setUpdateDetailPageId (toggle);
  }
  return (
    <Context.Provider
      value={{
        HandleDisplaySignUp,
        DisplaySignUp,
        HandleDisplayLogin,
        DisplayLogIn,
        HandleUpdateDetailPageId,
        UpdateDetailPageId,
      }}
    >
      {children}
    </Context.Provider>
  );
};
