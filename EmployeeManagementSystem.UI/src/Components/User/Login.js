import React, { useState, useContext } from "react";

import { Form, Button, Input, Label, Container } from "semantic-ui-react";
import { modalStoreContext } from "../../Stores/ModalStore";
import { UserStoreContext } from "../../Stores/UserStore";

export default function Login() {
  const modalStore = useContext(modalStoreContext);
  const userStore = useContext(UserStoreContext);
  const { closeModal } = modalStore;
  const { login } = userStore;

  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");
  const [submitting, setSubmitting] = useState(false);

  const required = (value) => {
    if (!value) {
      return (
        <div className="alert alert-danger" role="alert">
          This field is required!
        </div>
      );
    }
  };

  const submitForm = (e) => {
    e.preventDefault();
    setSubmitting(true);
    login({ email: userName, password: password })
      .then(() => closeModal())
      .then(() => setSubmitting(false));
  };

  return (
    <Container>
      <Form onSubmit={submitForm}>
        <Form.Field>
          <input
            placeholder="Email"
            onChange={(e) => setUserName(e.target.value)}
          />
        </Form.Field>
        <Form.Field>
          <input
            placeholder="Password"
            type="password"
            onChange={(e) => setPassword(e.target.value)}
          />
        </Form.Field>
        <Button
          loading={submitting}
          content="Login"
          type="submit"
          primary
          fluid
        ></Button>
      </Form>
    </Container>
  );
}
