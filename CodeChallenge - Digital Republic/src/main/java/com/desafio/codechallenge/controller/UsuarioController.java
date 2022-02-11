package com.desafio.codechallenge.controller;

import java.util.Optional;

import javax.transaction.Transactional;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.bind.annotation.PathVariable;

import com.desafio.codechallenge.model.UserDeposito;
import com.desafio.codechallenge.model.UserTransferencia;
import com.desafio.codechallenge.model.Usuario;
import com.desafio.codechallenge.service.UsuarioService;
import com.desafio.codechallenge.repository.UsuarioRepository;

@Transactional
@RestController
@RequestMapping("/usuario")
@CrossOrigin("*")
public class UsuarioController
{
	@Autowired
	private UsuarioService usuarioService;
	
	@Autowired
	private UsuarioRepository repository;
	
	@PostMapping("/cadastrar")
	public ResponseEntity<Usuario> Post(@RequestBody Usuario usuario)
	{
		return ResponseEntity.status(HttpStatus.CREATED)
				.body(usuarioService.CadastrarUsuario(usuario));
	}

	@PostMapping("/depositar")
	public ResponseEntity<Optional<UserDeposito>> Post(@RequestBody Optional<UserDeposito> deposito)
	{
		return ResponseEntity.status(HttpStatus.OK).body(usuarioService.Depositar(deposito));
	}

	@PostMapping("/transferir")
	public ResponseEntity<Optional<UserTransferencia>> Post(@RequestBody UserTransferencia transferencia)
	{
		return ResponseEntity.status(HttpStatus.OK).body(usuarioService.Transferir(transferencia));
	}

	@GetMapping("/saldo/{cpf}")
	public ResponseEntity<Optional<Usuario>> getByCpf(@PathVariable String cpf)
	{
		return ResponseEntity.ok(repository.findByCpf(cpf));
	}
}
