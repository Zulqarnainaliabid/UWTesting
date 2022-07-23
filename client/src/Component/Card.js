import React,{useContext} from 'react';
import {Context} from './Context';
import Button from 'react-bootstrap/Button';
import Card from 'react-bootstrap/Card';
import {Link} from 'react-router-dom';
function Cards (props) {
  const contextData = useContext (Context);
  return ( 
    <Card style={{width: '18rem'}}>
      <Link
        onClick={() => {
          window.scrollTo (0, 0);
          contextData.HandleUpdateDetailPageId(props.item.id)
        }}
        className="text-decoration-none"
        to="/details"
      >
        <Card.Img variant="top" src={props.image} />
      </Link>
      <Card.Body>
        <div className="CardContent">
          <Card.Title className='TitleDetailsPage'>{props.name}</Card.Title>
          <Card.Text className='StoryDetailsPage'>
            {props.storyLine}
          </Card.Text>
        </div>
        {props.item.selected
          ? <Button
              variant="primary mt-2 RemoveButton"
              onClick={() => {
                props.handleRemoveFromCart (props.id);
              }}
            >
              Remove From Cart
            </Button>
          : <Button
              variant="primary mt-2"
              onClick={() => {
                props.handleAddToCart (props.item, props.id);
              }}
            >
              Add to Cart
            </Button>}
      </Card.Body>
    </Card>
  );
}

export default Cards;
