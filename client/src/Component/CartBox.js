import React, {useEffect, useState} from 'react';
function CartBox (props) {
  const [Cart, setCart] = useState ([]);
  useEffect (() => {
    if (localStorage.getItem ('Cart') !== null) {
      let value = localStorage.getItem ('Cart');
      value = JSON.parse (value);
      setCart ([...value]);
    }
  }, [props.UpdateCart]);
  return (
    <div className="bg-white p-3 outerWrapperCart">
      {Cart.map ((item, index) => {
        return (
          <div key={index} className="d-flex FlexGap mt-3  BorderBottom pb-3 BorderColor">
            <div className="OuterWrapperCartImage">
              <img src={item.posterUrl} alt="cartImage" className='WidthHeight-100'/>
            </div>
            <div className="outerWrapperContentCart">
              <p className='TitleDetailsPage'>{item.title}</p>
              <p className='StoryDetailsPage'>{item.storyLine}</p>
            </div>
          </div>
        );
      })}
    </div>
  );
}

export default CartBox;
