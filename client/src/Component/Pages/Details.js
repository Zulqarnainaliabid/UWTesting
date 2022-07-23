import React, {useEffect, useState, useContext} from 'react';
import {Context} from '../Context';
import Header from '../Header';
import {handleGetMoviesDetails} from '../Services/APIs';
import DotLoader from 'react-spinners/DotLoader';
function Details () {
  const contextData = useContext (Context);
  const [DisplayLoader, setDisplayLoader] = useState (false);
  const [Detail, setDetail] = useState ('');
  async function handleGetDetails () {
    setDisplayLoader (true);
    let data = await handleGetMoviesDetails (contextData.UpdateDetailPageId);
    setDisplayLoader (false);
    setDetail (data);
    localStorage.setItem ('Details', JSON.stringify (data));
  }
  useEffect (
    () => {
      if (contextData.UpdateDetailPageId) {
        handleGetDetails ();
      } else {
        if (localStorage.getItem ('Details') !== null) {
          let value = localStorage.getItem ('Details');
          value = JSON.parse (value);
          setDetail (value);
        }
      }
      contextData.HandleUpdateDetailPageId (null);
    },
    [contextData.UpdateDetailPageId]
  );

  return (
    <div>
      <Header />
      <div className="p-5 ">
        <div className="d-flex p-5 outerWrapperContentDetailsPage">
          <div className="OuterWrapperImageDetailsPge">
            {!DisplayLoader
              ? <img
                  src={Detail.posterUrl}
                  alt="img"
                  className="WidthHeight-100"
                />
              : <DotLoader />}
          </div>
          <div className="mt-2">
            <p className="TitleDetailsPage">{Detail.title}</p>
            <p className="BudgetDetailsPage mt-2">${Detail.budget}</p>
            <p className="StoryDetailsPage mt-2">{Detail.storyLine}</p>
            <p className="StoryDetailsPage mt-2">{Detail.plot}</p>
          </div>
        </div>
        <hr />
        <div>
          <p className="BudgetDetailsPage">OVERVIEW:</p>
          <div className="ps-5 pe-5 pb-2">
            <p className="FontSize14 DarkColor">{Detail.plot}</p>
            <h6 className="mt-2 mb-2 DarkColor FontSize14">FEATURES:</h6>
            {Detail.genres &&
              Detail.genres.map ((item, index) => {
                return (
                  <div key={index} className="DarkColor FontSize14">
                    <p>GENRE: {item.value}</p>
                  </div>
                );
              })}
          </div>
        </div>
        <div>
          <p className="BudgetDetailsPage">Actors:</p>
          <div
            className="ps-5 pe-5 pb-2 DarkColor FontSize14 d-flex  flex-wrap"
            style={{gap: '25px'}}
          >
            {Detail.actors &&
              Detail.actors.map ((item, index) => {
                return (
                  <div key={index} className="text-center outerFrameActorImage">
                    <div>
                      <div className="outerWrapperActorImagesDetailsPage">
                        <img
                          src={item.person.imageURL}
                          alt="img"
                          className="ImagesActorDetailPage WidthHeight-100"
                        />
                      </div>
                      <p className="mt-2 mb-2">{item.person.fullName}</p>
                    </div>
                  </div>
                );
              })}
          </div>
        </div>
      </div>
    </div>
  );
}

export default Details;
