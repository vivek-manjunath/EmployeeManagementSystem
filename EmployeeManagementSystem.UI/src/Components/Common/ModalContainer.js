import React from "react";
import { Modal } from "semantic-ui-react";

export default function ModalContainer(props) {
  return (
    <Modal open={props.open} onClose={props.closeHandler} size="mini">
      <Modal.Content>{props.body}</Modal.Content>
    </Modal>
  );
}