import React, {
  useEffect,
  useState,
  forwardRef,
  useContext,
  useImperativeHandle,
} from 'react';
import {Context} from './Context';
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import shopping from './assets/shopping-cart.png';
import CartBox from './CartBox';
const Header = forwardRef ((props, ref) => {
  const contextData = useContext (Context);
  const [Cart, setCart] = useState ([]);
  const [DisplayCart, setDisplayCart] = useState (false);
  useEffect (
    () => {
      if (localStorage.getItem ('Cart') !== null) {
        let value = localStorage.getItem ('Cart');
        value = JSON.parse (value);
        setCart ([...value]);
      }
    },
    [props.UpdateCart]
  );
  useEffect (
    () => {
      const timer = setTimeout (() => {
        setDisplayCart (false);
      }, 4000);
      return () => clearTimeout (timer);
    },
    [DisplayCart]
  );
  useImperativeHandle (ref, () => ({
    getAlert () {
      setDisplayCart (true);
    },
  }));
  return (
    <Navbar
      collapseOnSelect
      expand="lg"
      bg="dark"
      variant="dark"
      className="PositionRelative"
    >
      <Container>
        <Navbar.Brand href="/">Home</Navbar.Brand>
        <Navbar.Toggle aria-controls="responsive-navbar-nav" />
        <Navbar.Collapse id="responsive-navbar-nav ">
          <Nav className="me-auto">
            <Nav.Link href="/actors">Actors</Nav.Link>
          </Nav>
          <Nav className="align-items-center FlexGap">
            <p
              className="text-white CursorPointer"
              onClick={() => {
                contextData.HandleDisplayLogin (true);
              }}
            >
              Login
            </p>
            <p
              className="text-white CursorPointer"
              onClick={() => {
                contextData.HandleDisplaySignUp (true);
              }}
            >
              Sign Up
            </p>
            {Cart.length &&
              <div className="outerWrapperCartLength text-white d-flex justify-content-center align-items-center">
                <p style={{fontSize: '13px'}}>{Cart.length}</p>
              </div>}
            <div
              className="outerWrapperShoppingCartIcon"
              onClick={() => {
                setDisplayCart (true);
              }}
            >
              <img
                src={shopping}
                alt="shopping"
                className='WidthHeight-100'
              />
            </div>
          </Nav>
        </Navbar.Collapse>
        {DisplayCart &&
          <div className="PositionAbsolute OuterWrapperCartBox">
            <CartBox UpdateCart={props.UpdateCart} />
          </div>}
      </Container>
    </Navbar>
  );
});
export default Header;
