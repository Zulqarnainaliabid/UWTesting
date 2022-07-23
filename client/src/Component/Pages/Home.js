import React, { useEffect, useState, useRef } from 'react';
import Cards from '../Card';
import { handleGetMoviesList } from '../Services/APIs';
import Header from '../Header';
function Home(props) {
  const childRef = useRef();
  const [UpdateCart, setUpdateCart] = useState(false);
  const [Movies, setMovies] = useState([]);
  const [Cart, setCart] = useState([]);
  async function HandleGetMovies() {
    let movies = await handleGetMoviesList();
    for (let i = 0; i < movies.length; i++) {
      movies[i].selected = false;
    }
    setMovies([...movies]);
    localStorage.setItem('Movies', JSON.stringify(Movies));
  }
  function handleAddToCart(data, id) {
    Cart.push(data);
    setCart(Cart);
    for (let i = 0; i < Movies.length; i++) {
      if (Movies[i].id === id) {
        Movies[i].selected = true;
        break;
      }
    }
    localStorage.setItem('Cart', JSON.stringify(Cart));
    localStorage.setItem('Movies', JSON.stringify(Movies));
    setMovies([...Movies]);
    childRef.current.getAlert()
    setUpdateCart(!UpdateCart);
  }
  function HandleGetCart() {
    if (localStorage.getItem('Cart') !== null) {
      let value = localStorage.getItem('Cart');
      value = JSON.parse(value);
      return value;
    }
  }
  function handleRemoveFromCart(id) {
    for (let i = 0; i < Movies.length; i++) {
      if (Movies[i].id === id) {
        Movies[i].selected = false;
        break;
      }
    }
    let TempCart = HandleGetCart();
    for (let i = 0; i < TempCart.length; i++) {
      if (TempCart[i].id === id) {
        TempCart.splice(i, 1);
        break;
      }
    }
    setCart([...TempCart]);
    localStorage.setItem('Cart', JSON.stringify(TempCart));
    localStorage.setItem('Movies', JSON.stringify(Movies));
    setMovies([...Movies]);
    setUpdateCart(!UpdateCart);
    childRef.current.getAlert()
  }
  useEffect(
    () => {
      if (localStorage.getItem('Movies') !== null) {
        let value = localStorage.getItem('Movies');
        value = JSON.parse(value);
        if (value.length < 1)
          HandleGetMovies();
        else
          setMovies([...value]);
      } else {
        HandleGetMovies();
      }
    },
    [props.UpdateCart]
  );
  return (
    <div>
      <Header UpdateCart={UpdateCart} ref={childRef} />
      <div className="MainContainer d-flex flex-wrap p-5 justify-content-center">
        {Movies.map((item, index) => {
          return (
            <Cards
              key={index}
              image={item.posterUrl}
              name={item.title}
              item={item}
              id={item.id}
              storyLine={item.storyLine}
              handleAddToCart={handleAddToCart}
              handleRemoveFromCart={handleRemoveFromCart}
            />
          );
        })}
      </div>
    </div>
  );
}

export default Home;
