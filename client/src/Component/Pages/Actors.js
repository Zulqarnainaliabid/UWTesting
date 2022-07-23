import React, {useState, useEffect} from 'react';
import {handleGetAllActor} from '../Services/APIs';
import Header from '../Header';
function Actors () {
  const [Actor, setActor] = useState ([]);
  async function GetAllActors () {
    let data = await handleGetAllActor ();
    setActor (data);
  }
  useEffect (() => {
    if (localStorage.getItem ('Actor') !== null) {
      let value = localStorage.getItem ('Actor');
      value = JSON.parse (value);
      setActor ([...value]);
    } else {
      GetAllActors ();
    }
  }, []);
  return (
    <div>
      <div className=" PositionRelative">
        <Header />
      </div>
      <div className="MainContainer d-flex flex-wrap p-5 justify-content-center">
        {Actor.map ((item, index) => {
          return (
            <div key={index}  className="d-flex FlexGap flex-wrap">
              <div  className='text-center'>
                <div className="outerWrapperActorsImages">
                  <img
                    src={item.imageURL}
                    alt="img"
                    className='WidthHeight-100'
                  />
                </div>
                <p className='ActorsName'>{item.fullName}</p>
              </div>
            </div>
          );
        })}
      </div>
    </div>
  );
}

export default Actors;
