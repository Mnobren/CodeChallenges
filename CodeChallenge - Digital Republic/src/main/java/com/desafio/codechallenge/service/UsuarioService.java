package com.desafio.codechallenge.service;

import java.util.Optional;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.desafio.codechallenge.model.UserDeposito;
import com.desafio.codechallenge.model.UserTransferencia;
import com.desafio.codechallenge.model.Usuario;
import com.desafio.codechallenge.repository.UsuarioRepository;

@Service
public class UsuarioService
{
	@Autowired
	private UsuarioRepository repository;
	
	public Usuario CadastrarUsuario(Usuario usuario)
	{
		usuario.setSaldo(0);
		return repository.save(usuario);
	}

	public Optional<UserDeposito> Depositar(Optional<UserDeposito> deposito)
	{
		Optional<Usuario> usuario = repository.findByCpf(deposito.get().getCpf());
		if(usuario.isPresent())
		{
			if(0 <= deposito.get().getValor() && deposito.get().getValor() <= 2000)
			{
				usuario.get().setSaldo((usuario.get().getSaldo())+(deposito.get().getValor()));
				return deposito;
			}
		}
		return null;
	}

	public Optional<UserTransferencia> Transferir(UserTransferencia transferencia)
	{
		Optional<Usuario> usuario = repository.findByCpf(transferencia.getCpf());
		if(usuario.isPresent())
		{
			long valor = transferencia.getValor();
			long saldo = usuario.get().getSaldo();
			if(valor < saldo)
			{
				Optional<Usuario> destino = repository.findByCpf(transferencia.getCpfDestino());
				if(destino.isPresent())
				{
					usuario.get().setSaldo(saldo - valor);
					destino.get().setSaldo(destino.get().getSaldo() + valor);
					Optional<UserTransferencia> t = Optional.of(transferencia);
					return t;
				}
			}
		}
		return null;
	}
}
