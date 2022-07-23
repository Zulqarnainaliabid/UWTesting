import React, {useState, useContext} from 'react';
import {Context} from '../Context';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
function Login (props) {
  const contextData = useContext (Context);
  const [validated, setValidated] = useState (false);
  const handleClose = () => contextData.HandleDisplayLogin (false);
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
      <Modal show={contextData.DisplayLogIn} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Login</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form noValidate validated={validated} onSubmit={handleSubmit}>
            <Row>
              <Form.Group className="mb-2" controlId="validationCustom01">
                <Form.Label>Email</Form.Label>
                <Form.Control required type="email" placeholder="Email" />
              </Form.Group>
              <Form.Group className="mb-2" controlId="validationCustom02">
                <Form.Label>Password</Form.Label>
                <Form.Control required type="password" placeholder="Password" />
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

export default Login;
