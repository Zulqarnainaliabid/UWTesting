import React, {useState, useContext} from 'react';
import {Context} from '../Context';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
export default function SignUp () {
  const contextData = useContext (Context);
  const [validated, setValidated] = useState (false);
  const handleClose = () => contextData.HandleDisplaySignUp (false);
  const handleSubmit = event => {
    const form = event.currentTarget;
    if (form.checkValidity () === false) {
      event.preventDefault ();
      event.stopPropagation ();
    }
    setValidated (true);
  };
  return (
    <div>
      <Modal show={contextData.DisplaySignUp} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Sign Up</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form noValidate validated={validated} onSubmit={handleSubmit}>
            <Row>
              <Form.Group  className='mb-2'  controlId="validationCustom01">
                <Form.Label>First name</Form.Label>
                <Form.Control
                  required
                  type="text"
                  placeholder="First name"
                />
              </Form.Group>
              <Form.Group  className='mb-2'   controlId="validationCustom02">
                <Form.Label>Last name</Form.Label>
                <Form.Control
                  required
                  type="text"
                  placeholder="Last name"
                />
              </Form.Group>
            </Row>
            <Row>
              <Form.Group className='mb-2'   controlId="validationCustom03">
                <Form.Label>Email</Form.Label>
                <Form.Control type="email" placeholder="Email" required />
                <Form.Control.Feedback type="invalid">
                  Please provide a valid Email.
                </Form.Control.Feedback>
              </Form.Group>
              <Form.Group className='mb-2'  controlId="validationCustom04">
                <Form.Label>Password</Form.Label>
                <Form.Control type="password" placeholder="Password" required />
                <Form.Control.Feedback type="invalid">
                  Please provide a valid Password.
                </Form.Control.Feedback>
              </Form.Group>
            </Row>
            <Form.Group className="mb-3">
              <Form.Check
                required
                label="Agree to terms and conditions"
                feedback="You must agree before submitting."
                feedbackType="invalid"
              />
            </Form.Group>
            <Button type="submit">Submit form</Button>
          </Form>
        </Modal.Body>
      </Modal>
    </div>
  );
}
